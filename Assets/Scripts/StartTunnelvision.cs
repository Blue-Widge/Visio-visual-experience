using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StartTunnelvision : LocomotionProvider
{ 
    public float impairmentTimer;
    public float fadeTime;

    public void Start()
    {
        locomotionPhase = LocomotionPhase.Idle;
    }

    public void Update()
    {
        if (impairmentTimer >= 0) impairmentTimer -= Time.deltaTime;
        if (impairmentTimer < 0)
        {
            locomotionPhase = LocomotionPhase.Moving;
        }
    }
}
