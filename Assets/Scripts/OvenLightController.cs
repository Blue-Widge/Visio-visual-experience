using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


/** @brief Class that handles the light inside of the oven to turn on when opened and off when closed */
public class OvenLightController : MonoBehaviour
{
    /** @brief Hinge joint of the oven's door */
    private HingeJoint _hingeJoint;

    public Light ovenLight;
    
    /** @brief Threshold to know when to turn on the oven's light */
    public int turnOnAngle;

    /** @brief Sets the hinge joint component */
    void Start()
    {
        _hingeJoint = GetComponent<HingeJoint>();        
    }

    /** @brief Detects the oven's door angle to set the light to on or off at each frame */
    void Update()
    {
        if (_hingeJoint.angle >= turnOnAngle)
        {
            ovenLight.enabled = true;
        } else
        {
            ovenLight.enabled = false;
        }
        
    }
}
