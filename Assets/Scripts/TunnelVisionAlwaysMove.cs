using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TunnelVisionAlwaysMove : LocomotionProvider
{
    public float impairmentTimer;
    public bool turnedOn = false;

    public void Start()
    {
        locomotionPhase = LocomotionPhase.Idle;
    }

    public void Update()
    {
        if (impairmentTimer >= 0) impairmentTimer -= Time.deltaTime;
        if (impairmentTimer < 0 && !turnedOn)
        {
            locomotionPhase = LocomotionPhase.Moving;
            turnedOn = true;
        }
    }
    public void changeLocoationPhase(LocomotionPhase phase)
    {
        locomotionPhase = phase;
    }
}
