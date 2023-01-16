using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimationHallway : MonoBehaviour
{
    public GameObject handPointer;

    private string hand = "Hand";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(hand))
        {
            handPointer.SetActive(false);
        }

    }
}
