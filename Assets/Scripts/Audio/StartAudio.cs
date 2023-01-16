using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource mandatoryPreviousSource;

    private bool played = false;

    private string hand = "Hand";
    private bool trigger = true;
    private float previousSourceLength = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (mandatoryPreviousSource != null)
        {
            trigger = false;
            previousSourceLength = mandatoryPreviousSource.clip.length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying) played = true;
        if (!trigger && mandatoryPreviousSource.isPlaying) trigger = true;
        if (trigger && previousSourceLength >= 0) previousSourceLength -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(hand) && !played && trigger && previousSourceLength <= 0)
        {
            AudioController.PlayAudio(audioSource, audioSource.clip, false);
        }
    }
}
