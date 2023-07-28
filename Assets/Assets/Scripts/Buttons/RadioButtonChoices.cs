using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class RadioButtonChoices : MonoBehaviour
{
    public GameObject fingerTip;
    bool isTouching;
    Vector3 fingerTipForward;
    [SerializeField] float touchDistance;


    // Start is called before the first frame update
    void Start()
    {
        fingerTipForward = fingerTip.transform.TransformDirection(Vector3.forward);
        isTouching = false;

    }

    // Update is called once per frame
    void Update()
    {
            if (Physics.Raycast(fingerTip.transform.position, fingerTipForward, out RaycastHit ray, touchDistance))
            {
                Collider rayCollider = ray.collider;
                
            }
    
    }
    


}