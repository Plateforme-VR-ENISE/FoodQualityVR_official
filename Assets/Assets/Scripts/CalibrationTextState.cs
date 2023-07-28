using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalibrationTextState : MonoBehaviour
{
    [SerializeField] Text calibrationState;
    [SerializeField] Animator calibrationStateAnimation;

   
    // Update is called once per frame
    public void CalibrationState()
    {
        calibrationState.text = "Calibration réalisée";
        calibrationState.color = Color.green;
        Debug.Log(calibrationState.text);
        calibrationStateAnimation.SetTrigger("go");
    }
}
