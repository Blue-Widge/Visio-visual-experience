using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudio : MonoBehaviour
{
    public AudioSource audioSource;
    private string hand = "Hand";
    public AudioSource mandatoryPreviousSource;

    private bool trigger;
    // Start is called before the first frame update
    void Start()
    {
        if (mandatoryPreviousSource == null)
        {
            trigger = true;
        } else
        {
            trigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!trigger && mandatoryPreviousSource.isPlaying)
        {
            trigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag(hand) && trigger)
        {
            audioSource.Play();
        }
    }
}
