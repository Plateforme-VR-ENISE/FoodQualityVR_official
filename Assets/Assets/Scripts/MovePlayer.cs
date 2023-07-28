using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    [SerializeField] float sensibility;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Vector3 position = this.transform.position;
            position.x= position.x + sensibility;
            this.transform.position = position;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Vector3 position = this.transform.position;
            position.x = position.x - sensibility;
            this.transform.position = position;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector3 position = this.transform.position;
            position.y = position.y + sensibility;
            this.transform.position = position;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector3 position = this.transform.position;
            position.y = position.y - sensibility;
            this.transform.position = position;

        }
       
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 position = this.transform.position;
            position.z = position.z - sensibility;
            this.transform.position = position;

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 position = this.transform.position;
            position.z = position.z + sensibility;
            this.transform.position = position;

        }
    }
}

