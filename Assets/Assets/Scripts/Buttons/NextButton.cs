using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
        Collider m_Collider;

    [SerializeField] AudioSource source;
    [SerializeField] Animator animatordoor;
    [SerializeField] Animator animatorfood;

    [SerializeField] Animator animatornext;

    [SerializeField] AudioClip slipandplate;
    [SerializeField] AudioClip validationsound;

    [SerializeField] Toggle defaultRadioButton;

    private bool readyToGetData = true;
    private GameObject plate;
    private GameObject questionnaires;


    // Start is called before the first frame update
    void Start()
    {
        m_Collider = GetComponent<Collider>();
        plate = GameObject.Find("plate");
        questionnaires = GameObject.Find("questionnaryButton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (readyToGetData ==  true);
        {
            readyToGetData =  false;
            m_Collider.enabled =  false;
            plate.GetComponent<Data>().stopTimer();
            StartCoroutine(Anotherfood());
        }
    }

     private IEnumerator Anotherfood()
    {
        source.clip = validationsound;
        // Playing the audio
        source.Play();
        // Animation for the questionnary
        animatornext.SetTrigger("go");
        // Animation for the food
        animatorfood.SetTrigger("goreverse");
        // Animation for the door
        animatordoor.SetTrigger("go");
        yield return new WaitForSeconds(6);
        defaultRadioButton.isOn=true;
        plate.GetComponent<Data>().startTimer();
        source.clip = slipandplate;
        // Playing the audio
        source.Play();
        plate.GetComponent<ObjFromFile>().ChangeFood();
        Debug.Log("CHANGEMENT");
        // Animation for the door
        animatordoor.SetTrigger("go");
        // Animation for the food
        animatorfood.SetTrigger("go");
        yield return new WaitForSeconds(7);
        animatornext.SetTrigger("go");
        m_Collider.enabled =  true;
        readyToGetData =  true;



    }

    

}
