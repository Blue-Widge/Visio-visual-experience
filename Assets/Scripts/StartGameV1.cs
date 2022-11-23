using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartGameV1 : MonoBehaviour
{
    public InputActionReference startExperienceReference = null;

    [SerializeField]
    private GameObject notebook;
    [SerializeField]
    private GameObject visualImpairment;

    private bool start;
    // Start is called before the first frame update
    void Start()
    {
        start = false;   
    }

    private void Awake()
    {
        startExperienceReference.action.started += StartKitchen;
    }

    private void OnDestroy()
    {
        startExperienceReference.action.started-= StartKitchen;
    }

    void StartKitchen(InputAction.CallbackContext context)
    {
        if (!start)
        {
            start = true;
            visualImpairment.SetActive(true);
            notebook.SetActive(false);
        }

    }
}
