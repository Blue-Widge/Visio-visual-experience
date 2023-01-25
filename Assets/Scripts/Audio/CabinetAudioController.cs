using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetAudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource mandatoryPreviousSource;
    private HingeJoint _hingeJoint;

    public int turnOnAngle;
    public bool playWhenSmaller;
    private bool _played = false;
    private float _previousSourceLength = 0;

    private bool _trigger = false;
    // Start is called before the first frame update
    void Start()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        _previousSourceLength = mandatoryPreviousSource.clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_trigger && mandatoryPreviousSource.isPlaying) _trigger = true;
        if (_trigger && _previousSourceLength >= 0) _previousSourceLength -= Time.deltaTime;
        if (audioSource.isPlaying) _played = true;
        if (_trigger && playWhenSmaller && _hingeJoint.angle <= turnOnAngle && !_played && _previousSourceLength <= 0)
        {
            _played = true;
            AudioController.PlayAudio(audioSource, audioSource.clip, false);
        }
        else if (_trigger && !playWhenSmaller && _hingeJoint.angle >= turnOnAngle && !_played && _previousSourceLength <= 0)
        {
            _played = true;
            AudioController.PlayAudio(audioSource, audioSource.clip, false);
        }
    }
}
