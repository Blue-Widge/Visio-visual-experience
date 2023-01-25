using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


/** @brief Class that handles the oral tutorial at the beginning of the experience */
public class StartAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource mandatoryPreviousSource;
    public VideoPlayer videoPlayer;

    private bool _played = false;

    private string _hand = "Hand";
    private bool _trigger = true;
    private float _previousSourceLength = 0;

    /** @brief Checks if the previous audio is set, if so set the length of the audio clip */
    void Start()
    {
        if (mandatoryPreviousSource != null)
        {
            _trigger = false;
            _previousSourceLength = mandatoryPreviousSource.clip.length;
        }
    }

    /** @brief Detects if the audio source has begun, if so decrease the length of the audio clip by the time */
    void Update()
    {
        if (audioSource.isPlaying) _played = true;
        if (!_trigger && mandatoryPreviousSource.isPlaying) _trigger = true;
        if (_trigger && _previousSourceLength >= 0) _previousSourceLength -= Time.deltaTime;
    }


    /** @brief Detects if the player pressed the button only after hearing the sound, if so play the trigger pressed preview
     \param[in] other Object that entered the trigger box
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_hand) && !_played && _trigger && _previousSourceLength <= 0)
        {
            videoPlayer.Play();
            AudioController.PlayAudio(audioSource, audioSource.clip, false);
        }
    }
}
