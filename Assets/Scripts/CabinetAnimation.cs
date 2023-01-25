using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private readonly string _hand = "Hand";
    private readonly string _trValueOpen = "TrOpen";
    private readonly string _trValueClose = "TrClose";
    private readonly string _state = "StateOpen";

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag(HAND))
    //    {
    //        if (!animator.GetBool(STATE))
    //        {
    //            animator.SetBool(STATE, true);
    //            animator.SetTrigger(TR_VALUE_OPEN);
    //        }
    //        else
    //        {
    //            animator.SetBool(STATE, false);
    //            animator.SetTrigger(TR_VALUE_CLOSE);
    //        }
    //    }
    //}

    public void OpenCabinet()
    {
        Debug.Log("Called");
        if (!animator.GetBool(_state))
        {
            animator.SetBool(_state, true);
            animator.SetTrigger(_trValueOpen);
        }
        else
        {
            animator.SetBool(_state, false);
            animator.SetTrigger(_trValueClose);
        }
    }
}
