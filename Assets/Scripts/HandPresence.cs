using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

/**
 * @brief Class that detects hands inputs to trigger animations
 */
public class HandPresence : MonoBehaviour
{
    /** @brief Contains characteristics to differentiate a device from another*/
    public InputDeviceCharacteristics characteristics;
    /** @brief Contains all the detected devices */
    private List<InputDevice> _inputDevices;
    /** @brief Contains the concerned controlled */
    private InputDevice _inputDevice;
    /** @brief Hand animator */
    private Animator _deviceAnimator;
    
    /** @brief Detect if the controllers are being simulated, if not start the DetectDevices() coroutine*/
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("XR Device Simulator"))
        {
            Debug.Log("Device Simulator is enabled");
            this.enabled = false;
            return;
        }
        Debug.Log("Device Simulator is disabled");
        _inputDevices = new List<InputDevice>();
        StartCoroutine(GetDevices(1.0f));
    }
    
    /** @brief Detect during the whole experience the devices, at all time in case controllers are being disconnected and reconnected
     * \param[in] delayTime time to wait before trying to detect again any device connected
     */
    IEnumerator GetDevices(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        InputDevices.GetDevices(_inputDevices);

        foreach (var item in _inputDevices)
            Debug.Log(item.name + item.characteristics);

        if (_inputDevices.Count > 1)
        {
            InputDevices.GetDevicesWithCharacteristics(characteristics, _inputDevices);
            _inputDevice = _inputDevices[0];
            _deviceAnimator = GetComponent<Animator>();
        }
    }
    
    /** @brief Call the UpdateHandAnimator function if an animator is on the hand */
    void Update()
    {
        if (_deviceAnimator != null)
            UpdateHandAnimator();
    }
    
    /** @brief Change the hand animator depending on the controller inputs */
    void UpdateHandAnimator()
    {
        if (_inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            _deviceAnimator.SetFloat("Trigger", triggerValue);
        else
            _deviceAnimator.SetFloat("Trigger", .0f);

        if (_inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            _deviceAnimator.SetFloat("Grip", gripValue);
        else
            _deviceAnimator.SetFloat("Grip", .0f);
    }
}
