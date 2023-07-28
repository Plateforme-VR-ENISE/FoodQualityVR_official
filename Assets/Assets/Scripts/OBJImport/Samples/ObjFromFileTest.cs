using Dummiesman;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class ObjFromFileTest : MonoBehaviour
{
    [System.Serializable]
    public struct Foods
    {
        public string name;
        public int nbNiveauQualite;
        public string[] nbVertices;
        public string[] sizeTexture;
        public string[] pseudoMos;

        public Vector3 localPosition;
        public Vector3 localRotation;
        public Vector3 localScale;
        

        

    }

    public GameObject plateFoods;

    public Foods[] nbFoods = new Foods[2];

    public bool readList = false;

    public bool secondsessions = false;
    private int positionToStartReadFood;
    public string listFileToRead ; 


    void Awake()
    {
        ReadFoodSettings();
        ReadFoodLocalPostionRotationScale();
        ImportFile();
        DeleteFileForSecondSession();

        
    }
      void ImportFile()
        {
        for (int y = 0; y < nbFoods.Length; y++)
        {
            Debug.Log(nbFoods.Length);
        

            for (int i = 0; i < nbFoods[y].nbNiveauQualite; i++)
            {

                //file path

                string filePath = Application.dataPath + @"\Assets\Models\FOR_SCREENSHOTS\1-20\" + nbFoods[y].name + "\\" + nbFoods[y].name + "_" + nbFoods[y].sizeTexture[i] + "\\" + nbFoods[y].name + "_" + nbFoods[y].nbVertices[i] + ".obj";
                print(filePath);
                string filePathMtl = filePath + ".mtl";
                //load

                var loadedObj = new OBJLoader().Load(filePath, filePathMtl);
                loadedObj.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial.shader = Shader.Find("Unlit/Texture");
                loadedObj.transform.localPosition = nbFoods[y].localPosition;
                loadedObj.transform.rotation = Quaternion.Euler(nbFoods[y].localRotation);
                //Quaternion.RotateTowards(transform.rotation, nbFoods[y].gameObject.transform.rotation, 0); 
                loadedObj.transform.localScale = nbFoods[y].localScale;
                loadedObj.transform.SetParent(plateFoods.transform, false);
                loadedObj.tag = nbFoods[y].name;
                loadedObj.name = loadedObj.tag.ToString() + (i+1).ToString();
                loadedObj.layer = LayerMask.NameToLayer("Recorded");
                //reduce global frame rate; 

                var childForMosValue = new GameObject();
                childForMosValue.transform.parent = loadedObj.transform;
                childForMosValue.name = nbFoods[y].pseudoMos[i];
            }
        }

            

            if (readList == true)
            {
            for (int i = 0; i < plateFoods.transform.childCount-1; i++)
            {
            var objToOrganize = new GameObject();
            string[] rows = File.ReadAllLines(Application.dataPath.ToString() + ("/Assets/Models/FOR_SCREENSHOTS/randomized_listes/").ToString() + (listFileToRead) + ".csv");
            var name_object = rows[i];
            objToOrganize = GameObject.Find(name_object.Replace(";", ""));
            Debug.Log(objToOrganize.name);
            objToOrganize.transform.SetSiblingIndex(i+1);
            }
            
        }
        }


          void ReadFoodSettings()
          {
            for (int y = 0; y < nbFoods.Length; y++)
            {
            string[] Lines = System.IO.File.ReadAllLines(Application.dataPath.ToString() + ("/Assets/Models/FOR_SCREENSHOTS/predicted_mos_28_02_2022_test/ChosenFoods_").ToString() + (nbFoods[y].name) + ".csv");
            for (int i = 0; i < nbFoods[y].nbNiveauQualite; i++)
            {
            string[] Columns = Lines[i].Split(';');  
            nbFoods[y].nbVertices[i] = Columns[2];
            nbFoods[y].sizeTexture[i]  = Columns[3];
            nbFoods[y].pseudoMos[i]  = Columns[5];
        
            }
          }

          }
          void ReadFoodLocalPostionRotationScale()
          {
            for (int y = 0; y < nbFoods.Length; y++)
            {
            string[] Lines = System.IO.File.ReadAllLines(Application.dataPath.ToString() + ("/Assets/Models/FOR_SCREENSHOTS/foods_localPositionRotationScale/").ToString() + (nbFoods[y].name) + ".csv");
            string[] Position= Lines[1].Split(';');
            string[] Rotation= Lines[2].Split(';');
            string[] Scale = Lines[3].Split(';');

            nbFoods[y].localPosition = new Vector3(
             float.Parse(Position[0]),
             float.Parse(Position[1]),
             float.Parse(Position[2]));

            nbFoods[y].localRotation = new Vector3(
             float.Parse(Rotation[0]),
             float.Parse(Rotation[1]),
             float.Parse(Rotation[2]));

            nbFoods[y].localScale = new Vector3(
             float.Parse(Scale[0]),
             float.Parse(Scale[1]),
             float.Parse(Scale[2]));

            }



          }
        void DeleteFileForSecondSession()
          {
          if (secondsessions ==true)
          {
            for (int y=1; y <= (plateFoods.transform.childCount-1)/2; y++)
            {
            Destroy(plateFoods.transform.GetChild(y).gameObject);
            }
          }
          
          else
          {
            for (int y=((plateFoods.transform.childCount-1)/2)+1; y <= (plateFoods.transform.childCount-1); y++)
            {
            Destroy(plateFoods.transform.GetChild(y).gameObject);
            }
          }
          GameObject.Find("progression").GetComponent<Progression>().GetTotal();
          
          
    }
    }


    



