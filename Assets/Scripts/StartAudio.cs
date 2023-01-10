using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudio : MonoBehaviour
{
    public AudioSource audioSource;
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
            audioSource.Play();
        }
    }
}
