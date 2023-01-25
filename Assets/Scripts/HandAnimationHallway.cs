using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief Class that disable the hallway hand animation when the button is pressed*/
public class HandAnimationHallway : MonoBehaviour
{
    /** @brief The hand animation */
    public GameObject handPointer;

    /** @brief Tag to compare to detect if the collided object is a hand */
    private string _hand = "Hand";

    /** @brief Disable the hand preview when the collided object is the user's hand */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_hand))
        {
            handPointer.SetActive(false);
        }
    }
}
