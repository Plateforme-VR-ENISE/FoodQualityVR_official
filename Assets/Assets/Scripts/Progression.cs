using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progression : MonoBehaviour
{
    private Text text;
    [SerializeField] GameObject plate;
    private int totalFood;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Progression : " + (plate.GetComponent<ObjFromFile>().food_Rank).ToString() + " / " + (totalFood).ToString() ;
    }

    public void GetTotal()
    {

            totalFood = ((plate.transform.childCount)-1)/2;
        
    }
}
