#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on Sun 11 Nov 07:16:04 2018

@author: Alex Resiga
Re-modified TensorFlow classification file according to our need.
"""
import tensorflow as tf
import sys
import os, glob
import csv
import shutil

# Disable tensorflow compilation warnings
os.environ['TF_CPP_MIN_LOG_LEVEL']='2'

'''
Classify images from test folder and predict dog breeds along with score.
'''
def classify_image(image_path, headers):
    f = open('submit.csv','w')
    writer = csv.DictWriter(f, fieldnames = headers)
    writer.writeheader()
    
    # Loads label file, strips off carriage return
    label_lines = [line.rstrip() for line
                   in tf.gfile.GFile("trained_model/retrained_labels.txt")]
    # Unpersists graph from file
    with tf.gfile.FastGFile("trained_model/retrained_graph.pb", 'rb') as f:
        graph_def = tf.GraphDef()
        graph_def.ParseFromString(f.read())
        _ = tf.import_graph_def(graph_def, name='')

    files = os.listdir(image_path)
    with tf.Session() as sess:
         for file in files:
                print(file)
                if file == ".DS_Store":
                    continue
                # Read the image_data
                image_data = tf.gfile.FastGFile(image_path+'/'+file, 'rb').read()
                # Feed the image_data as input to the graph and get first prediction
                softmax_tensor = sess.graph.get_tensor_by_name('final_result:0')

                predictions = sess.run(softmax_tensor, \
                                       {'DecodeJpeg/contents:0': image_data})

                # Sort to show labels of first prediction in order of confidence
                top_k = predictions[0].argsort()[-len(predictions[0]):][::-1]
                
                
                records = []
                row_dict = {}
                head, tail = os.path.split(file)
                row_dict['id'] = tail.split('.')[0]

                for node_id in top_k:
                    human_string = label_lines[node_id]
                    
                    score = predictions[0][node_id]
                    print('%s (score = %.5f)' % (human_string, score))
                    row_dict[human_string] = score
                    
                    
                records.append(row_dict.copy())
                print("\n\n\n")
                # writer.writerows(records)
    f.close()    

def main():
    files = glob.glob('testset/*')
    for f in files:
        os.remove(f)
    try:
        files = glob.glob('uploads/*')
        files.sort(key=os.path.getmtime, reverse=True)
        current_file = files[0]
        image_name = current_file.split('/')[1]
        shutil.move(current_file, 'testset/'+image_name)
        
    except IndexError:
        print("no image available to classify")
    test_data_folder = 'testset/'
    template_file = open('sample.csv','r')
    d_reader = csv.DictReader(template_file)

    # get fieldnames from DictReader object and store in list
    headers = d_reader.fieldnames
    template_file.close()
    classify_image(test_data_folder, headers)
    

if __name__ == '__main__':
    main()
