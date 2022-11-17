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
    private Animator deviceAnimator;

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
            deviceAnimator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHandAnimator();
    }

    void UpdateHandAnimator()
    {
        if (inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            deviceAnimator.SetFloat("Trigger", triggerValue);
        else
            deviceAnimator.SetFloat("Trigger", .0f);

        if (inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            deviceAnimator.SetFloat("Grip", gripValue);
        else
            deviceAnimator.SetFloat("Grip", .0f);
    }
}
