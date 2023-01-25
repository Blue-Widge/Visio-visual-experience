using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
