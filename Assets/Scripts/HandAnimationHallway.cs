using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimationHallway : MonoBehaviour
{
    public GameObject handPointer;

    private string _hand = "Hand";

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
        if (other.CompareTag(_hand))
        {
            handPointer.SetActive(false);
        }

    }
}
