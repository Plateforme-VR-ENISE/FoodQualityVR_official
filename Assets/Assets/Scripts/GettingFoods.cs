using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Text; // Contains classes that represent ASCII and Unicode character encodings; abstract base classes for converting blocks of characters to and from blocks of bytes; and a helper class that manipulates and formats String objects without creating intermediate instances of String.
using System.Threading; // Provides classes and interfaces that enable multithreaded programming. In addition to classes for synchronizing thread activities and access to data (Mutex, Monitor, Interlocked, AutoResetEvent, and so on), this namespace includes a ThreadPool class that allows you to use a pool of system-supplied threads, and a Timer class that executes callback methods on thread pool threads.
using System.IO; // Contains types that allow reading and writing to files and data streams, and types that provide basic file and directory support.
using Dummiesman;

public class GettingFoods : MonoBehaviour

{
   

    


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

    public GameObject plateFoods;


    [SerializeField] GameObject plancheFoods;

    public int food_Rank = 1;
    private List<GameObject> ListgameObjects = new List<GameObject>();
    GameObject actualfoods;

    // Use this for initialization

    void Start()
    {
        source = GetComponent<AudioSource>();
        
       

        ListgameObjects.Add(plateFoods.gameObject.transform.GetChild(0).gameObject); // ne pas retirer erreur sinon          
        for (int y = 1; y <= plateFoods.transform.childCount; y++)
        {
            ListgameObjects.Add(plateFoods.gameObject.transform.GetChild(y - 1).gameObject);
            plateFoods.gameObject.transform.GetChild(y - 1).gameObject.SetActive(false);
        }
        ListgameObjects.RemoveAt(0);

        Debug.Log("nb de gameobjects " + ListgameObjects.Count);

      

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