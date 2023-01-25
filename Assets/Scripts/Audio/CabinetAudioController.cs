using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief Class that handles the sound played to explain to the user how to interact withe the cabinets*/
public class CabinetAudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource mandatoryPreviousSource;

    /** @brief Joint of the cabinet's door, to detect when it is being opened or not */
    private HingeJoint _hingeJoint;

    public int turnOnAngle;
    public bool playWhenSmaller;
    private bool _played = false;
    private float _previousSourceLength = 0;

    private bool _trigger = false;

    /** @brief Sets the hinge joint of the cabinet and the clip length of the first source */
    void Start()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        _previousSourceLength = mandatoryPreviousSource.clip.length;
    }

    /** @brief Plays the explanation to the user to explain the controls, and once the audio finished if the user opens a cabinet it plays a congrats to the user */
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
