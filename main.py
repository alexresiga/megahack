import requests
import wget 
from bs4 import BeautifulSoup
import re
import os
r = requests.get("https://www.emag.ro/telefoane-mobile/c?ref=bc")
soup = BeautifulSoup(r.content, "html.parser")
contor = 0
for a in soup.find_all(href=re.compile("telefon")):
    if "https" in a['href'] and "mobile" not in a['href']:
        r = requests.get(a['href'])
        mini_soup = BeautifulSoup(r.content, "html.parser")
        b = mini_soup.find(class_="page-title")
        #print(b.text.strip(''))
        b = b.text.strip()
        img_name = ''.join(b.replace(',','').strip().split(" ")[2:])
        
        ceva  = ''.join(''.join(b.strip().split(" ")[2:]).split(",")[:1])
        print(img_name)
        for b in mini_soup.find_all(class_=re.compile("product-gallery-inner")):
           
            for c in b.find_all(href=re.compile("images")):
                
                if not os.path.exists("images/" + ceva):
                    os.makedirs("images/" + ceva)
                    contor = 0
                #if not os.path.exists('/Users/alex/Documents/megahack/images/{}/{}.{}.jpg'.format(ceva, img_name, str(contor))):
                contor = contor + 1
                wget.download(c['href'], '/Users/alex/Documents/megahack/images/{}/{}.{}.jpg'.format(ceva, img_name, str(contor)))