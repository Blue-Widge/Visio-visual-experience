using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/** @brief Class that tricks the locomotion system to think the player is always moving to use the XR Interaction toolkit's vignette  */
public class TunnelVisionAlwaysMove : LocomotionProvider
{
    /** @brief Time before enabling the impairment */
    public float impairmentTimer;
    public bool turnedOn = false;


    /** @brief Set the user movements to "Idle" */
    public void Start()
    {
        locomotionPhase = LocomotionPhase.Idle;
    }

    /** @brief Set the locomotion to "Moving" after the impairment delay */
    public void Update()
    {
        if (impairmentTimer >= 0) impairmentTimer -= Time.deltaTime;
        if (impairmentTimer < 0 && !turnedOn)
        {
            locomotionPhase = LocomotionPhase.Moving;
            turnedOn = true;
        }
    }

    public void ChangeLocoationPhase(LocomotionPhase phase)
    {
        locomotionPhase = phase;
    }
}
