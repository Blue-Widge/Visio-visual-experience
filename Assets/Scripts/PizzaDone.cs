using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PizzaDone : MonoBehaviour
{
    public GameObject pizza;
    public HingeJoint ovenHingeJoint;
    public float hingeAngle = -85;
    public TunnelVisionAlwaysMove locomotionProviderAlwaysMove;
    private bool _finished = false;
    public AudioSource ovenAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Pizza" && ovenHingeJoint.angle < hingeAngle && !_finished)
        {
            _finished = true;
            StartCoroutine(FinishGame());
        }
    }

    IEnumerator FinishGame()
    {
        ovenAudioSource.Play();
        yield return new WaitForSeconds(9f);
        locomotionProviderAlwaysMove.ChangeLocoationPhase(LocomotionPhase.Done);
    }
}
