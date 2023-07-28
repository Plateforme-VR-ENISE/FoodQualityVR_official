using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text; // Contains classes that represent ASCII and Unicode character encodings; abstract base classes for converting blocks of characters to and from blocks of bytes; and a helper class that manipulates and formats String objects without creating intermediate instances of String.
using System.Threading; // Provides classes and interfaces that enable multithreaded programming. In addition to classes for synchronizing thread activities and access to data (Mutex, Monitor, Interlocked, AutoResetEvent, and so on), this namespace includes a ThreadPool class that allows you to use a pool of system-supplied threads, and a Timer class that executes callback methods on thread pool threads.
using System.IO; // Contains types that allow reading and writing to files and data streams, and types that provide basic file and directory support.



public class Data : MonoBehaviour
{
    float timeValue;
    string timeOfApparition;
    bool start, stop;
    int CountSaving = 0;
    string date_correct_date;
    string date_correct_time;

    public string toggleMark;

    string[] rowDataTemp = new string[13];
    private List<string[]> rowData = new List<string[]>();

    [SerializeField] string nameFileToSave;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Date : " + System.DateTime.UtcNow.ToLocalTime().ToString());
        date_correct_date = System.DateTime.UtcNow.ToLocalTime().ToString().Replace("/", "_");
        date_correct_time = date_correct_date.Replace(":", "_");
        Debug.Log(date_correct_time);

        timeValue = 0;
        start = false;
        stop = false;

        rowDataTemp = new string[13];
        rowDataTemp[0] = "id";
        rowDataTemp[1] = "range";
        rowDataTemp[2] = "time_spent (s)";
        rowDataTemp[3] = "vertices_number";
        rowDataTemp[4] = "triangles_number";
        rowDataTemp[5] = "name";
        rowDataTemp[6] = "tag";
        rowDataTemp[7] = "width_texture";
        rowDataTemp[8] = "height_texture";
        rowDataTemp[9] = "mark";         
        rowDataTemp[10] = "pseudo_mos"; 
        rowDataTemp[11] = "level";
        rowDataTemp[12] = "time of apparition"; 

           
           
           
        rowData.Add(rowDataTemp);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (start == true)
        {
            timeValue += Time.deltaTime;
            timeOfApparition = (System.DateTime.Now).ToString("dd'/'MM'/'yyyy HH:mm:ss:fff");
        }
    }

    void OnApplicationQuit()
    {
        stopTimer();
    }
  

    public void startTimer()
    {
        start = true;
    }

    public void stopTimer() 
    {

        start = false;
        double timesaved = System.Math.Round(timeValue, 2);
        Debug.Log("Timer : " + timesaved.ToString());
        timeValue = 0;

        rowDataTemp = new string[13];
        rowDataTemp[0] = nameFileToSave[3].ToString()  + nameFileToSave[4].ToString() ;
        rowDataTemp[1] =  CountSaving.ToString() ; // name
        rowDataTemp[2] = timesaved.ToString();
        rowDataTemp[3] = this.GetComponent<ObjFromFile>().actualFoodNumberOfVertices.ToString();
        rowDataTemp[4] = this.GetComponent<ObjFromFile>().actualFoodNumberOfTriangles.ToString();
        rowDataTemp[5] = this.GetComponent<ObjFromFile>().actualFoodName;
        rowDataTemp[6] = this.GetComponent<ObjFromFile>().actualFoodTag;
        rowDataTemp[7] = this.GetComponent<ObjFromFile>().actualFoodNumberTextureWidth.ToString();
        rowDataTemp[8] = this.GetComponent<ObjFromFile>().actualFoodNumberTextureHeight.ToString();
        rowDataTemp[9] = toggleMark;
        rowDataTemp[10] = this.GetComponent<ObjFromFile>().actualFoodPseudoMos;
        rowDataTemp[11] = this.GetComponent<ObjFromFile>().actualFoodName.Substring(this.GetComponent<ObjFromFile>().actualFoodName.Length - 1);
        rowDataTemp[12] = timeOfApparition.ToString();

        rowData.Add(rowDataTemp);



        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        { 
            output[i] = rowData[i];
        }
        int length = output.GetLength(0);
        string delimiter = ";";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();

        CountSaving++;


    }

    // Following method is used to retrive the relative path as device platform
    private string getPath()
    {
        //return Application.dataPath + "/CSVfiles/" + "timer_food_"+ date_correct_time + ".csv";
        return Application.dataPath + "/CSVfiles/" + nameFileToSave +".csv";
    }
}




    



 




