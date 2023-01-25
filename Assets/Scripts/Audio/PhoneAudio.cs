using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief Class that handles the phone audio playing */
public class PhoneAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource mandatoryPreviousSource;

    public AudioClip ringingAudio;
    public AudioClip explanationAudio;

    public GameObject book;
    public GameObject button;

    private bool _played = false;
    private bool _playedRinging = false;

    private string _hand = "Hand";
    private bool _trigger = true;
    private float _previousSourceLength = 0;

    /** @brief Checks if the previous audio is set, if so set the length of the audio clip.*/
    void Start()
    {
        if (mandatoryPreviousSource != null)
        {
            _trigger = false;
            _previousSourceLength = mandatoryPreviousSource.clip.length;
        }
    }

    /** @brief 	Detect if the audio source has begun, if so decrease the length of the audio clip by the time. 
      * Once the first audio ended, play the phone ringing
      */
    void Update()
    {
        //if (audioSource.isPlaying) played = true;
        if (!_trigger && mandatoryPreviousSource.isPlaying)
        {
            _trigger = true;
        }
        if (_trigger && _previousSourceLength >= 0) _previousSourceLength -= Time.deltaTime;
        if (!_playedRinging && _previousSourceLength <= 0)
        {
            AudioController.PlayAudio(audioSource, ringingAudio, true);
            _playedRinging = true;
        }

    }

    /** @brief Detect when the touch the phone once it's ringing, if so play the phone conversation audio 
     * \param[in] Object that entered the trigger box
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_hand) && !_played && _trigger && _previousSourceLength <= 0)
        {
            AudioController.PlayAudio(audioSource, explanationAudio, false);
            button.SetActive(false);
            book.SetActive(true);
        }
    }
}
