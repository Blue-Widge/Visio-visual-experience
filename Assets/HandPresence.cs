using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class HandPresence : MonoBehaviour
{
    private List<InputDeviceCharacteristics> inputCharacteristics = new List<InputDeviceCharacteristics>() 
        { InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller, 
        InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller };

    private List<InputDevice> inputDevices = new List<InputDevice>();
    private InputDevice leftController;
    private InputDevice rightController;

// Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("XR Device Simulator"))
        {
            Debug.Log("Device Simulator is enabled");
            gameObject.SetActive(false);
            return;
        }
        Debug.Log("Device Simulator is disabled");

        InputDevices.GetDevices(inputDevices);
        Debug.Log(inputDevices.Count);
        if (inputDevices.Count <= 1)
            return;
        foreach (var item in inputDevices)
            Debug.Log(item.name + item.characteristics);

        InputDevices.GetDevicesWithCharacteristics(inputCharacteristics[0], inputDevices);
        leftController = inputDevices[0];
        InputDevices.GetDevicesWithCharacteristics(inputCharacteristics[1], inputDevices);
        rightController = inputDevices[0];

        if (leftController.isValid && rightController.isValid)
        {
            Debug.Log("Les deux manettes lessgo");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
