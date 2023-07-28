import pymeshlab
import os

currentFolder = os.getcwd()

## Fonction pour simplifier un mesh
def meshSimplification(folder, fileName, newFileName, divfaces, level):
    objPath = currentFolder + '/' + folder + '/'
    objFile = objPath + fileName
    newObjFile = objPath + newFileName
    nb_level_LOD = level


    ms = pymeshlab.MeshSet()
    ms.load_new_mesh(objFile)
    face_nb = ms.current_mesh().face_number()
    print('nombre de faces au départ :', face_nb)

    for i in range(0,nb_level_LOD):
        # face_nb = ms.current_mesh().face_number()
        # delta_simpl = (face_nb - min_nb_faces)/nb_level_LOD
        target_face_num =ms.current_mesh().face_number()/divfaces
        ms.simplification_quadric_edge_collapse_decimation_with_texture(targetfacenum = int(target_face_num))

        # ms.save_current_mesh(newObjFile + str(nb_level_LOD-i) + 'of'+ str(nb_level_LOD+1) + '.obj')
        ms.save_current_mesh(newObjFile + str(ms.current_mesh().face_number()) + '.obj')

        print('\n',newFileName + str(nb_level_LOD-i) + 'of'+ str(nb_level_LOD+1) + '.obj', '\nnombre de faces :', ms.current_mesh().face_number())

meshSimplification('muffin_2048','muffin_142940.obj','muffin_', 1.2, 50)
