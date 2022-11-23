using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameV2 : MonoBehaviour
{

    [SerializeField]
    private GameObject visualImpairment;

    // Start is called before the first frame update
    void Start()
    {
        //Start the visual impairment
        visualImpairment.SetActive(true);
        //Start the minigame

    }
}
