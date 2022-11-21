using System.Collections;
using System.Collections.Generic;
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
    private bool isOpen = false;
    private bool hiddenContent = false;
    JointSpring spring;
    float startingAngle;
    Vector3 startingPosition;
    public float sensibility;

    delegate void DetectDoorOpen();
    DetectDoorOpen detectDoor;

    // Start is called before the first frame update
    void Start()
    {
        if (!(GetComponent<BoxCollider>() || GetComponent<MeshCollider>() || GetComponent<CapsuleCollider>()))
        {
            Debug.LogWarning("A collider is needed to hide the content of the container");
            this.enabled = false;
            return;
        }
        if (content.Count == 0)
        {
            Debug.LogWarning("Please assign content of container to hide, put a parent for faster results (inside container is often used)");
            this.enabled = false;
            return;
        }

        if (containerHingeJoint)
        {
            if (containerSpringJoint)
            {
                Debug.LogError("You can't set a hingeJoint and springJoint at the same time");
                this.enabled = false;
                return;
            }
            detectDoor = detectAngleDoorOpen;
            spring = new JointSpring();
            spring.spring = 2f;
            containerHingeJoint.spring = spring;
            containerHingeJoint.useSpring = true;
            startingAngle = containerHingeJoint.angle;
        }
        else
        {
            if (!containerSpringJoint)
            {
                Debug.LogError("You must set a hingeJoint or a springJoint");
                this.enabled = false;
                return;
            }
            detectDoor = detectDrawerOpen;
            startingPosition = transform.position;
        }

        if (!outsideContainer)
            outsideContainer = transform.parent;
        if (!insideContainer)
            insideContainer = transform;
    }

    // Update is called once per frame
    void Update()
    {
        detectDoor();
        if (hiddenContent && isOpen)
        {
            foreach(GameObject item in content)
            {
                item.SetActive(true);
            }
            hiddenContent = false;
        }
        if (!(hiddenContent || isOpen))
        {
            foreach (GameObject item in content)
            {
                item.SetActive(false);
            }
            hiddenContent = true;
        }
    }

    void detectAngleDoorOpen()
    {
        isOpen = Mathf.Abs(containerHingeJoint.angle - startingAngle) > sensibility;
        containerHingeJoint.useSpring = !(containerHingeJoint.angle > 85f);
    }
    void detectDrawerOpen()
    {
        isOpen = Mathf.Abs(transform.position.x - startingPosition.x) > sensibility;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(transform.name + " Collided " + other.name + ' ' + other.gameObject.layer);
        other.transform.parent = (other.gameObject.layer == 6) && !other.gameObject.GetComponent<XRGrabInteractable>().isSelected ? insideContainer : other.transform.parent;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(transform.name + " Stopped colliding " + ' ' + other.name + other.gameObject.layer);
        other.transform.parent = (other.gameObject.layer == 6) ? outsideContainer : other.transform.parent;
    }
}
