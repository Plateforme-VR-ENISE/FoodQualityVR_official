using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleOnOff : MonoBehaviour
{
        Toggle m_Toggle;
        string m_Toggle_text;

    // Start is called before the first frame update
    void Start()
    {
                m_Toggle = GetComponent<Toggle>();
                m_Toggle_text = this.transform.GetChild(1).GetComponent<Text>().text.ToString();
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        m_Toggle.isOn=true;
        GameObject.Find("plate").GetComponent<Data>().toggleMark=m_Toggle_text;

    }
}
