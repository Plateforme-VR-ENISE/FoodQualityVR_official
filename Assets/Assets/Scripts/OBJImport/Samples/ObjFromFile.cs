using Dummiesman;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Text; // Contains classes that represent ASCII and Unicode character encodings; abstract base classes for converting blocks of characters to and from blocks of bytes; and a helper class that manipulates and formats String objects without creating intermediate instances of String.
using System.Threading; // Provides classes and interfaces that enable multithreaded programming. In addition to classes for synchronizing thread activities and access to data (Mutex, Monitor, Interlocked, AutoResetEvent, and so on), this namespace includes a ThreadPool class that allows you to use a pool of system-supplied threads, and a Timer class that executes callback methods on thread pool threads.
using System.IO; // Contains types that allow reading and writing to files and data streams, and types that provide basic file and directory support.


public class ObjFromFile : MonoBehaviour
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

    public int actualFoodNumberOfVertices;
    public int actualFoodNumberOfTriangles;
    public int actualFoodNumberTextureWidth;
    public int actualFoodNumberTextureHeight;
    public string actualFoodName;
    public string actualFoodTag;
    public string actualFoodPseudoMos;

    [SerializeField] AudioSource source;
    [SerializeField] GameObject RightHand;
    [SerializeField] GameObject LeftHand;

    [SerializeField] GameObject plancheFoods;

    public int food_Rank = 1;
    private List<GameObject> ListgameObjects = new List<GameObject>();
    GameObject actualfoods;
    void Awake()
    {
        ReadFoodSettings();
        ReadFoodLocalPostionRotationScale();
        ImportFile();
        DeleteFileForSecondSession();
        InitializationListFoodsToDisplay();

        
    }
      void ImportFile()
        {
        for (int y = 0; y < nbFoods.Length; y++)
        {
            Debug.Log(nbFoods.Length);
        

            for (int i = 0; i < nbFoods[y].nbNiveauQualite; i++)
            {

                //file path

                string filePath = Application.dataPath + @"\Assets\Models\food_models\" + nbFoods[y].name + "\\" + nbFoods[y].name + "_" + nbFoods[y].sizeTexture[i] + "\\" + nbFoods[y].name + "_" + nbFoods[y].nbVertices[i] + ".obj";
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
            string[] rows = File.ReadAllLines(Application.dataPath.ToString() + ("/Assets/Models/food_models/randomized_listes/").ToString() + (listFileToRead) + ".csv");
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
            string[] Lines = System.IO.File.ReadAllLines(Application.dataPath.ToString() + ("/Assets/Models/food_models/predicted_mos_and_datas/ChosenFoods_").ToString() + (nbFoods[y].name) + ".csv");
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
            string[] Lines = System.IO.File.ReadAllLines(Application.dataPath.ToString() + ("/Assets/Models/food_models/foods_localPositionRotationScale/").ToString() + (nbFoods[y].name) + ".csv");
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

    void InitializationListFoodsToDisplay()

    {
          source = GetComponent<AudioSource>();
        
       
        
        ListgameObjects.Add(plateFoods.gameObject.transform.GetChild(0).gameObject); // ne pas retirer erreur sinon   

        if (secondsessions ==true)
        {
        for (int y = 36; y <= plateFoods.transform.childCount; y++)
        {
            ListgameObjects.Add(plateFoods.gameObject.transform.GetChild(y - 1).gameObject);
            plateFoods.gameObject.transform.GetChild(y - 1).gameObject.SetActive(false);
        }
        ListgameObjects.RemoveAt(0);

        Debug.Log("nb de gameobjects " + ListgameObjects.Count);
        }
        else
        {
           {
        for (int y = 1; y <= plateFoods.transform.childCount; y++)
        {
            ListgameObjects.Add(plateFoods.gameObject.transform.GetChild(y - 1).gameObject);
            plateFoods.gameObject.transform.GetChild(y - 1).gameObject.SetActive(false);
        }
        ListgameObjects.RemoveAt(0);

        Debug.Log("nb de gameobjects " + ListgameObjects.Count);
        } 
        }
      

        actualfoods = ListgameObjects[food_Rank];

        actualfoods.SetActive(true);
        actualfoods.transform.SetParent(plateFoods.transform, false);


        plancheFoods.SetActive(true);
        if (actualfoods.tag == "boeuf" | actualfoods.tag == "sushis")
        {
            plateFoods.GetComponent<Renderer>().enabled = false;
            plancheFoods.GetComponent<Renderer>().enabled = true;

        }
        else
        {
            plateFoods.GetComponent<Renderer>().enabled = true;
            plancheFoods.GetComponent<Renderer>().enabled = false;
        }

        plancheFoods.transform.SetParent(plateFoods.transform, true);


        GettingFoodsGraphicProperties();
    }

    // Update is called once per frame
    void GettingFoodsGraphicProperties()

    {
        actualFoodNumberOfVertices = actualfoods.transform.GetChild(0).GetComponent<MeshFilter>().mesh.vertexCount;
        actualFoodNumberOfTriangles = actualfoods.transform.GetChild(0).GetComponent<MeshFilter>().mesh.triangles.Length/3;
        actualFoodName = actualfoods.name;
        actualFoodTag = actualfoods.tag;
        actualFoodNumberTextureWidth= actualfoods.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture.height;
        actualFoodNumberTextureHeight= actualfoods.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture.height;
        actualFoodPseudoMos = actualfoods.transform.GetChild(1).name;

      
    }


    public void ChangeFood()
    {
        
        
        actualfoods.SetActive(false);
        actualfoods.transform.SetParent(plateFoods.transform, false);

        food_Rank++;
        actualfoods=ListgameObjects[food_Rank];
        actualfoods.SetActive(true);
        actualfoods.transform.SetParent(plateFoods.transform, false);
        GettingFoodsGraphicProperties();

        if (actualfoods.tag == "boeuf" | actualfoods.tag == "sushis")
        {
            plateFoods.GetComponent<Renderer>().enabled = false;
            plancheFoods.GetComponent<Renderer>().enabled = true;

        }
        else
        {
            plateFoods.GetComponent<Renderer>().enabled = true;
            plancheFoods.GetComponent<Renderer>().enabled = false;
        }
    }
    }


    



