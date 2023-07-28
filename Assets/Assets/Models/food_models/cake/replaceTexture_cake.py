import re
import os
import shutil

currentFolder = os.getcwd()
mtlFiles = []

## Fonction pour dupliquer un dossier
def copytree(source, destination, symlinks=False, ignore=None):
    if not os.path.exists(destination):
        os.makedirs(destination)
    for item in os.listdir(source):
        s = os.path.join(source, item)
        d = os.path.join(destination, item)
        if os.path.isdir(s):
            copytree(s, d, symlinks, ignore)
        else:
            if not os.path.exists(d) or os.stat(s).st_mtime - os.stat(d).st_mtime > 1:
                shutil.copy2(s, d)

## Fonction pour changer le nom du fichier png de tous les fichiers mtl d'un dossier donné
def replaceTexture(folder,oldTexture,newTexture):
    for file in os.listdir(folder):
        if file.endswith(".mtl"): # récupération de tous les fichiers mtl du dossier
            mtlFiles.append(file)
    print(mtlFiles)
    for filename in mtlFiles :
        filePath = folder + '\\' + filename

        with open(filePath, 'r+') as f:
            text = f.read()
            text = re.sub(str(oldTexture), str(newTexture), text)
            f.seek(0)
            f.write(text)
            f.truncate()

copytree((currentFolder+ r'\cake_2048') , (currentFolder+ r'\cake_1024'))
replaceTexture(currentFolder+ r'\cake_1024',2048,1024)
copytree((currentFolder+ r'\cake_2048') , (currentFolder+ r'\cake_512'))
replaceTexture(currentFolder+ r'\cake_512',2048,512)
copytree((currentFolder+ r'\cake_2048') , (currentFolder+ r'\cake_256'))
replaceTexture(currentFolder+ r'\cake_256',2048,256)

