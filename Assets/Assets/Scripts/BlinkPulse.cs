using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BlinkPulse : MonoBehaviour {

    [SerializeField] SpriteRenderer sr;

    public float minimum = 0.3f;
    public float maximum = 1f;
    public float cyclesPerSecond = 2.0f;
    private float a;
    private bool increasing = true;
    [SerializeField] Animator blinkPulseAnimator;

    [SerializeField] Animator startAnimation;

    [SerializeField] Button startButton;
    public float calibrationTime;
    Color color;    
    Collider m_Collider;

    void Start() {
        
        m_Collider = startButton.GetComponent<Collider>();

        color = sr.color;
        a = maximum;

        
    }



    void Update() {
        float t = Time.deltaTime;
        if (a >= maximum) increasing = false;
        if (a <= minimum) increasing = true;
        a = increasing ? a += t * cyclesPerSecond * 2 : a -= t * cyclesPerSecond;
        color.a = a;
        sr.color = color;

        if (Input.GetKeyDown(KeyCode.E))
        {
             StartCoroutine(CalibrationTime());
        }
    }

   private IEnumerator CalibrationTime()
   {
        blinkPulseAnimator.SetTrigger("go");
        yield return new WaitForSeconds(calibrationTime);
        blinkPulseAnimator.SetTrigger("goreverse");
        startAnimation.SetTrigger("go");
        m_Collider.enabled =  true;


   }
}