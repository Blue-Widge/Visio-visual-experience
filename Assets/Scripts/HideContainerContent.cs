using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/** @brief Class that handles the hiding of the items contained inside a container*/
public class HideContainerContent : MonoBehaviour
{
    /** @brief List of all the items contained */
    public List<GameObject> content;
    /** @brief Variable that contains the Hinge joint component if a door  */
    public HingeJoint containerHingeJoint;
    /** @brief Variable that contains the Spring joint component if a drawer  */
    public SpringJoint containerSpringJoint;
    /** @brief Parent of the items contained by the container */
    public Transform insideContainer;
    /** @brief Parent of the items contained by the container */
    public Transform outsideContainer;

    /** @brief Boolean that says if the container is opened or not */
    [SerializeField]
    private bool isOpen;
    /** @brief Boolean that says if the items contained are hidden or not */
    private bool _hiddenContent;
    /** @brief JointSpring variable to set when the container's door should close by iteself */
    private JointSpring _spring;
    /** @brief The angle where the door is closed if a door */
    private float _startingAngle;
    /** @brief The position of the closed drawer if a drawer */
    private Vector3 _startingPosition;
    /** @brief Threshold to know if the container should be considered opened or closed */
    public float sensibility;

    /** @brief Delegate function that detects set to DetectAngleDoorOpen if a door and DetectDrawerOpen if a drawer*/
    private delegate void DetectDoorOpen();
    /** @brief Variable that contains the delegated function */
    private DetectDoorOpen _detectDoor;

    /**@brief Id needed to know which hand preview is linked to the container */
    public int id;

    /**@brief Detect if the necessary components are set and set the _detectDoor variable to the right function aswell as the startingPosition or angle variables*/
    private void Start()
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

    /** @brief Detects at each frame is the door is opened, hide or unhide the objects contained depending on the result */
    private void Update()
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
        if (isOpen) { EventSystemHandler.Current.ContainerDoorOpened(id); }
    }

    /**@brief Method that detects the opening of the door depending on the angles (cabinet or fridge) */
    private void DetectAngleDoorOpen()
    {
        isOpen = Mathf.Abs(containerHingeJoint.angle - _startingAngle) > sensibility;
        containerHingeJoint.useSpring = !(containerHingeJoint.angle > 85f);
    }

    /**@brief Method that detects the opening of the door depending on the position (drawer) */
    private void DetectDrawerOpen()
    {
        isOpen = Mathf.Abs(transform.position.x - _startingPosition.x) > sensibility;
    }

    /**@brief Method changes the item's parent to the container if it's a grabbable item and isn't held 
     /\param[in] other Object that entered the trigger box
    */
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

    /**@brief Method changes the item's parent to the scene or set outside container if it's a grabbable item
     /\param[in] other Object that entered the trigger box
    */
    private void OnTriggerExit(Collider other)
    {
        var otherGameObject = other.gameObject;
        other.transform.parent = (otherGameObject.layer == 6) ? outsideContainer : other.transform.parent;
        Debug.Log("gameobject : " + otherGameObject.name + " exited");
    }
}
