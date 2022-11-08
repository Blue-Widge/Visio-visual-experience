using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Doorify : MonoBehaviour
{
    public List<Collider> colliders;
    public Transform attachTransform;
    [SerializeField]
        XRGrabInteractable interactable;

    // Start is called before the first frame update
    void Start()
    {
        interactable = new XRGrabInteractable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
