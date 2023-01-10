using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LeafHandler : XRGrabInteractable
{
    private Renderer leafRenderer;
    private Rigidbody leafRigidbody;
    private MeshCollider leafCollider;
    static List<GameObject> leafList;
    bool leafAdded = false;

    protected override void Awake()
    {
        leafRigidbody = GetComponent<Rigidbody>();
        leafRigidbody.isKinematic = true;
        leafRenderer = GetComponent<Renderer>();
        leafRenderer.enabled = false;
        leafCollider = GetComponent<MeshCollider>();
        leafCollider.isTrigger = true;
        if (leafList == null)
        {
            leafList = new List<GameObject>();
            leafList.Add(this.gameObject);
        }
        base.Awake();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        //Doesn't spawn a leaf if already spawned
        if (leafAdded)
        {
            base.OnSelectEntering(args);
            return;
        }
        
        GameObject leafGameobject = Instantiate(this.gameObject, transform.position, transform.rotation, transform.parent.transform);

        //Prevent the player to crash the game by spawning infinite leaves
        leafList.Add(leafGameobject);
        if (leafList.Count > 10)
        {
            GameObject.Destroy(leafList[0]);
            leafList.RemoveAt(0);
        }
        Debug.Log(leafList.Count);

        leafRenderer.enabled = true;
        leafRigidbody.isKinematic = false;
        leafRigidbody.useGravity = true;
        
        base.OnSelectEntering(args);
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        leafCollider.isTrigger = false;
    }
}
