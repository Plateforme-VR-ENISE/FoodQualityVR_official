using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAndNextKeyboard : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] Animator animatordoor;
    [SerializeField] Animator animatorfood;

    [SerializeField] AudioClip slipandplate;
    [SerializeField] AudioClip validationsound;
    [SerializeField] Animator animatornext;


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            print("START");
            StartCoroutine(FirstFood());
        }


        if (Input.GetKeyDown(KeyCode.N))
        {
            
            print("Next button collider.enabled = ");
            StartCoroutine(Anotherfood());

            this.GetComponent<Data>().stopTimer();
            this.GetComponent<Data>().startTimer();
        }
    }
    private IEnumerator Anotherfood()
    {
        source.clip = validationsound;
        // Playing the audio
        source.Play();
        // Animation for the food
        animatorfood.SetTrigger("go");
        // Animation for the door
        animatordoor.SetTrigger("go");
        yield return new WaitForSeconds(6);
        source.clip = slipandplate;
        // Playing the audio
        source.Play();
        GameObject.Find("Slider").GetComponent<GettingFoods>().ChangeFood();
        Debug.Log("CHANGEMENT");
        // Animation for the door
        animatordoor.SetTrigger("go");
        // Animation for the food
        animatorfood.SetTrigger("go");
    }

        private IEnumerator FirstFood()
        {

            // Animation for the door
            animatordoor.SetTrigger("go");
            // Playing the audio
            source.Play();
            // Animation for the food
            animatorfood.SetTrigger("go");
            // Increment of the switch case counter
            this.GetComponent<Data>().startTimer();
            yield return new WaitForSeconds(20);
            animatornext.SetTrigger("go");

        }


}
