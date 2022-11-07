using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private readonly string HAND = "Hand";
    private readonly string TR_VALUE_OPEN = "TrOpen";
    private readonly string TR_VALUE_CLOSE = "TrClose";
    private readonly string STATE = "StateOpen";

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
        if (!animator.GetBool(STATE))
        {
            animator.SetBool(STATE, true);
            animator.SetTrigger(TR_VALUE_OPEN);
        }
        else
        {
            animator.SetBool(STATE, false);
            animator.SetTrigger(TR_VALUE_CLOSE);
        }
    }
}
