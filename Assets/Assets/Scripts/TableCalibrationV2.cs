using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TableCalibrationV2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject leftController;
    [SerializeField]GameObject rightController;
    [SerializeField]GameObject leftPointForCalibration;
    [SerializeField]GameObject rightPointForCalibration;
    [SerializeField]GameObject envrionnementToCalibrate;
    [SerializeField]GameObject toAttached;

    private Vector3 fromVector;
    private Vector3 toVector;

void Start()
{
      
    StartCoroutine(Calib());
       
}
    // Update is called once per frame
    public void Calibration()
    {
        


    
    }
IEnumerator Calib()
{
        yield return new WaitForSeconds(1f);
        envrionnementToCalibrate.transform.position = new Vector3((leftController.transform.position.x + rightController.transform.position.x)/2 ,((leftController.transform.position.y + rightController.transform.position.y)/2) - 0.1f/2, (leftController.transform.position.z + rightController.transform.position.z)/2);
        fromVector = (rightPointForCalibration.transform.position-leftPointForCalibration.transform.position);
        Debug.Log(fromVector);
        toVector = (rightController.transform.position-leftController.transform.position);
        Debug.Log(toVector);
        envrionnementToCalibrate.transform.rotation = Quaternion.FromToRotation(fromVector, toVector);  
}
    
}

