using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StartAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource mandatoryPreviousSource;
    public VideoPlayer videoPlayer;

    private bool _played = false;

    private string _hand = "Hand";
    private bool _trigger = true;
    private float _previousSourceLength = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (mandatoryPreviousSource != null)
        {
            _trigger = false;
            _previousSourceLength = mandatoryPreviousSource.clip.length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying) _played = true;
        if (!_trigger && mandatoryPreviousSource.isPlaying) _trigger = true;
        if (_trigger && _previousSourceLength >= 0) _previousSourceLength -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_hand) && !_played && _trigger && _previousSourceLength <= 0)
        {
            videoPlayer.Play();
            AudioController.PlayAudio(audioSource, audioSource.clip, false);
        }
    }
}
