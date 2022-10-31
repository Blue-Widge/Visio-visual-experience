using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics characteristics;
    private List<InputDevice> inputDevices;
    private InputDevice inputDevice;

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
        inputDevices = new List<InputDevice>();
        StartCoroutine(GetDevices(1.0f));
    }

    IEnumerator GetDevices(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        InputDevices.GetDevices(inputDevices);

        foreach (var item in inputDevices)
            Debug.Log(item.name + item.characteristics);

        if (inputDevices.Count > 1)
        {
            InputDevices.GetDevicesWithCharacteristics(characteristics, inputDevices);
            inputDevice = inputDevices[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            Debug.Log("primary button pressed");

        if (inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.2f)
            Debug.Log("trigger pressed");

        if (inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisVector) && primary2DAxisVector != Vector2.zero)
            Debug.Log("primary 2d axis : " + primary2DAxisVector);

    }
}
