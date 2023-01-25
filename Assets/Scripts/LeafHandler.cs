using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

/** @brief Class that generates a leaf when the user grabs one from the basil plant limited to 10 in scene */
public class LeafHandler : XRGrabInteractable
{
    private Renderer _leafRenderer;
    private Rigidbody _leafRigidbody;
    private MeshCollider _leafCollider;
    /** @brief list of all the generated leaves */
    static List<GameObject> _leafList;
    /** @brief boolean that prevent generated leaves to generate other leaves */
    bool _leafAdded = false;

    /** @brief Set the leafRenderer, rigidbody and collider, the list of leaves and the awake from XRGrabInteractable*/
    protected override void Awake()
    {
        _leafRigidbody = GetComponent<Rigidbody>();
        _leafRigidbody.isKinematic = true;
        _leafRenderer = GetComponent<Renderer>();
        _leafRenderer.enabled = false;
        _leafCollider = GetComponent<MeshCollider>();
        _leafCollider.isTrigger = true;
        if (_leafList == null)
        {
            _leafList = new List<GameObject>();
            _leafList.Add(this.gameObject);
        }
        base.Awake();
    }

    /** @brief Function that generates a leaf when the user wants to grab a leaf, and then grab it 
     /\param[in] args Argumentations from the XRGrabInteractable passed down
    */
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        //Doesn't spawn a leaf if already spawned
        if (_leafAdded)
        {
            base.OnSelectEntering(args);
            return;
        }
        
        GameObject leafGameobject = Instantiate(this.gameObject, transform.position, transform.rotation, transform.parent.transform);

        //Prevent the player to crash the game by spawning infinite leaves
        _leafList.Add(leafGameobject);
        if (_leafList.Count > 10)
        {
            GameObject.Destroy(_leafList[0]);
            _leafList.RemoveAt(0);
        }
        Debug.Log(_leafList.Count);

        _leafRenderer.enabled = true;
        _leafRigidbody.isKinematic = false;
        _leafRigidbody.useGravity = true;
        
        base.OnSelectEntering(args);
    }
    /** @brief Activates the physics of the leaf when the user finally drops it 
     /\param[in] args Argumentations from the XRGrabInteractable passed down
    */
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        _leafCollider.isTrigger = false;
    }
}
