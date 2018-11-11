from flask import Flask, request, render_template 
import os, glob
from sightengine.client import SightengineClient

def empty_directory():
    files = glob.glob(UPLOAD_FOLDER+'/*')
    for f in files:
        os.remove(f)


app = Flask(__name__)

client = SightengineClient('921284130', 'sSaKoX5FsqYiomVEt44o')

UPLOAD_FOLDER = '/Users/alex/Documents/megahack/phone_classification/uploads'
app.config['UPLOAD_FOLDER'] = UPLOAD_FOLDER
@app.route('/')
def hello_world():
    return render_template('index.html')

@app.route('/upload', methods = ['POST'])
def upload_file():
    file = request.files['image']
    filename = os.path.join(app.config['UPLOAD_FOLDER'], file.filename)
    file.save(filename)
    invalidImage = False
    output = client.check('nudity','wad','offensive').set_file(filename)
    if output['nudity']['safe'] <= output['nudity']['partial'] and output['nudity']['safe'] <= output['nudity']['raw']:
        invalidImage = True
        print('nud')
    
    if output['offensive']['prob'] > 0.5:
        invalidImage = True
        print('off')
    if invalidImage:
        os.remove(filename)
        
    return render_template('index.html', invalidImage=invalidImage, init=True)
