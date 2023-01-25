using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


/** @brief Class that handles the end of the pizza mini-game */
public class PizzaDone : MonoBehaviour
{
    public GameObject pizzaGameobject;
    public HingeJoint ovenHingeJoint;
    public float hingeAngle = -85;
    public TunnelVisionAlwaysMove locomotionProviderAlwaysMove;
    private bool _finished = false;
    public AudioSource ovenAudioSource;

    /** @brief Function that detects if the pizza is in the oven and the door is closed before launching the end of the game
     /\param[in] other The object that stayed in the trigger box
    */
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Pizza" && ovenHingeJoint.angle < hingeAngle && !_finished)
        {
            _finished = true;
            StartCoroutine(FinishGame());
        }
    }
    /** @brief Coroutine that plays the oven sound, wait 9 seconds before ending the game*/
    IEnumerator FinishGame()
    {
        ovenAudioSource.Play();
        yield return new WaitForSeconds(9f);
        locomotionProviderAlwaysMove.ChangeLocoationPhase(LocomotionPhase.Done);
    }
}
