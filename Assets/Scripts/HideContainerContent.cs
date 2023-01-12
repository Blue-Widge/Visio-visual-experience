using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HideContainerContent : MonoBehaviour
{
    public List<GameObject> content;
    public HingeJoint containerHingeJoint;
    public SpringJoint containerSpringJoint;
    public Transform insideContainer;
    public Transform outsideContainer;

    [SerializeField]
    private bool isOpen;
    private bool _hiddenContent;
    private JointSpring _spring;
    private float _startingAngle;
    private Vector3 _startingPosition;
    public float sensibility;

    private delegate void DetectDoorOpen();
    private DetectDoorOpen _detectDoor;
    //for the event an id is mandatory to disable the right preview
    public int id;


    // Start is called before the first frame update
    void Start()
    {
        if (!(GetComponent<BoxCollider>() || GetComponent<MeshCollider>() || GetComponent<CapsuleCollider>()))
        {
            Debug.LogWarning("A collider is needed to hide the content of the container");
            this.enabled = false;
            return;
        }

        if (!insideContainer)
        {
            Debug.LogWarning("An inside container is needed to know which gameobject to disable");
            this.enabled = false;
            return;
        }

        if (content.Count == 0)
            content.Add(insideContainer.gameObject);

        if (containerHingeJoint)
        {
            if (containerSpringJoint)
            {
                Debug.LogError("You can't set a hingeJoint and springJoint at the same time - Object : " + name);
                this.enabled = false;
                return;
            }
            _detectDoor = DetectAngleDoorOpen;
            _spring = new JointSpring {spring = 2f};
            containerHingeJoint.spring = _spring;
            _startingAngle = containerHingeJoint.angle;
        }
        else
        {
            if (!containerSpringJoint)
            {
                Debug.LogError("You must set a hingeJoint or a springJoint - Object : " + name);
                this.enabled = false;
                return;
            }
            _detectDoor = DetectDrawerOpen;
            _startingPosition = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _detectDoor();

        if (_hiddenContent && isOpen)
        {
            foreach(GameObject item in content)
            {
                item.SetActive(true);
            }
            _hiddenContent = false;
        }
        if (!(_hiddenContent || isOpen))
        {
            foreach (GameObject item in content)
            {
                item.SetActive(false);
            }
            _hiddenContent = true;
        }
        if (isOpen) { EventSystemHandler.current.ContainerDoorOpened(id); }
    }

    void DetectAngleDoorOpen()
    {
        isOpen = Mathf.Abs(containerHingeJoint.angle - _startingAngle) > sensibility;
        containerHingeJoint.useSpring = !(containerHingeJoint.angle > 85f);
    }
    void DetectDrawerOpen()
    {
        isOpen = Mathf.Abs(transform.position.x - _startingPosition.x) > sensibility;
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherGameObject = other.gameObject;
        if (!otherGameObject.GetComponentInParent<XRGrabInteractable>() && otherGameObject.layer == 6)
        {
            Debug.LogWarning("No XR Grab interactable component for : " + otherGameObject.name + " (or parents) layer : " + otherGameObject.layer);
            return;
        }

        other.transform.parent = (otherGameObject.layer == 6 ) && 
                                 !otherGameObject.GetComponentInParent<XRGrabInteractable>().isSelected && 
                                  !other.transform.parent.gameObject.CompareTag("Container") ? insideContainer : other.transform.parent;
    }

    private void OnTriggerExit(Collider other)
    {
        var otherGameObject = other.gameObject;
        other.transform.parent = (otherGameObject.layer == 6) ? outsideContainer : other.transform.parent;

        
    }
}
