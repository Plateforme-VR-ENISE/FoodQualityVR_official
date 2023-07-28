using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    Collider m_Collider;
    Collider m_Collider_Next;
    Collider m_Collider_stand;

    [SerializeField] AudioSource source;
    [SerializeField] Animator animatordoor;
    [SerializeField] Animator animatorfood;
    [SerializeField] Animator animatornext;


    [SerializeField] Animator animatorStart;

    [SerializeField] AudioClip startSound;
    private GameObject plate;

    // Start is called before the first frame update
    void Start()
    {
        m_Collider = GetComponent<Collider>();
        m_Collider_Next = GameObject.Find("nextButton").GetComponent<Collider>();
        m_Collider_stand = GameObject.Find("mainStructure").GetComponent<Collider>();
        plate = GameObject.Find("plate");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
            print("START");
            plate.GetComponent<Data>().startTimer();
            StartCoroutine(FirstFood());
    }

     private IEnumerator FirstFood()
    {
            animatordoor.SetTrigger("go");
            // Playing the audio
            source.Play();
            // Animation for the food
            animatorfood.SetTrigger("go");
            // Increment of the switch case counter
            animatorStart.SetTrigger("goreverse");
            m_Collider.enabled =  false;
            yield return new WaitForSeconds(7);
            animatornext.SetTrigger("go");
            m_Collider_Next.enabled =  true;
            m_Collider_stand.enabled = true;

    }

    

}
