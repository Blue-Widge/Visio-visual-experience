using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CabinetAudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource mandatoryPreviousSource;
    private HingeJoint hingeJoint;

    public int turnOnAngle;
    public bool playWhenSmaller;
    private bool played = false;
    private float previousSourceLength = 0;

    private bool trigger = false;
    // Start is called before the first frame update
    void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();
        previousSourceLength = mandatoryPreviousSource.clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        if (!trigger && mandatoryPreviousSource.isPlaying) trigger = true;
        if (trigger && previousSourceLength >= 0) previousSourceLength -= Time.deltaTime;
        if (audioSource.isPlaying) played = true;
        if (trigger && playWhenSmaller && hingeJoint.angle <= turnOnAngle && !played)
        {
            played = true;
            AudioController.PlayAudio(audioSource, audioSource.clip, false);
        }
        else if (trigger && !playWhenSmaller && hingeJoint.angle >= turnOnAngle && !played)
        {
            played = true;
            AudioController.PlayAudio(audioSource, audioSource.clip, false);
        }
    }
}
