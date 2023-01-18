using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetAudioController : MonoBehaviour
{
    public AudioSource audioSource;
    private HingeJoint hingeJoint;
    public int turnOnAngle;
    public bool playWhenSmaller;
    private bool played = false;
    // Start is called before the first frame update
    void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying) played = true;
        if (playWhenSmaller && hingeJoint.angle <= turnOnAngle && !played)
        {
            played = true;
            AudioController.PlayAudio(audioSource, audioSource.clip, false);
        }
        else if (!playWhenSmaller && hingeJoint.angle >= turnOnAngle && !played)
        {
            played = true;
            AudioController.PlayAudio(audioSource, audioSource.clip, false);
        }
    }
}
