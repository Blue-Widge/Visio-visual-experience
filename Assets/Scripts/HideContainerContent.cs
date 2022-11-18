using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HideContainerContent : MonoBehaviour
{
    public List<GameObject> content;
    public HingeJoint hingeJoint;
    public SpringJoint springJoint;
    public BoxCollider insideCollider;

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
        if (hingeJoint)
        {
            if (springJoint)
            {
                Debug.LogError("You can't set a hingeJoint and springJoint at the same time");
                this.enabled = false;
                return;
            }
            detectDoor = detectAngleDoorOpen;
            spring = new JointSpring();
            spring.spring = 2f;
            hingeJoint.spring = spring;
            hingeJoint.useSpring = true;
            startingAngle = hingeJoint.angle;
        }
        else
        {
            if (!springJoint)
            {
                Debug.LogError("You must set a hingeJoint or a springJoint");
                this.enabled = false;
                return;
            }
            detectDoor = detectDrawerOpen;
            startingPosition = transform.position;
        }

        if (!insideCollider)
            insideCollider = GetComponent<BoxCollider>();
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
        isOpen = Mathf.Abs(hingeJoint.angle - startingAngle) > sensibility;
        hingeJoint.useSpring = !(hingeJoint.angle > 85f);
    }
    void detectDrawerOpen()
    {
        isOpen = Mathf.Abs(transform.position.x - startingPosition.x) > sensibility;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(transform.name + " Collided " + other.name + other.tag);
        other.transform.parent = (other.gameObject.layer == 6) && !other.gameObject.GetComponent<XRGrabInteractable>().isSelected ? insideContainer : other.transform.parent;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(transform.name + " Stopped colliding " + other.name);
        other.transform.parent = (other.gameObject.layer == 6) ? outsideContainer : other.transform.parent;
    }
}
