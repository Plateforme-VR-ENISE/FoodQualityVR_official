using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class HandAnimationController : MonoBehaviour
{
    ActionBasedController controller;

    public HandAnimate handAnimate;

    private void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    private void Update()
    {
        handAnimate.SetGrip(controller.selectActionValue.action.ReadValue<float>());
        handAnimate.SetTrigger(controller.activateAction.action.ReadValue<float>());
    }
}
