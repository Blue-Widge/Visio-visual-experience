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
    private bool finished = false;
    public AudioSource ovenAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Pizza" && ovenHingeJoint.angle < hingeAngle && !finished)
        {
            finished = true;
            StartCoroutine(finishGame());
        }
    }

    IEnumerator finishGame()
    {
        yield return new WaitForSeconds(9f);
        ovenAudioSource.Play();
        locomotionProviderAlwaysMove.changeLocoationPhase(LocomotionPhase.Done);
    }
}
