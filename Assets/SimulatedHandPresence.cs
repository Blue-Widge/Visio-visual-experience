using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;


public class SimulatedHandPresence : HandPresence
{
    InputDevice leftController;
    InputDevice rightController;

    // Start is called before the first frame update
    void Start()
    {
        leftController = XRController.leftHand;
        rightController = XRController.rightHand;

        Debug.Log(leftController.name + leftController.device.description);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
