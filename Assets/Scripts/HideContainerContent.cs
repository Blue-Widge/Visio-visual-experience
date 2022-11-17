using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class HideContainerContent : MonoBehaviour
{
    public List<GameObject> content;
    public HingeJoint joint;
    public BoxCollider insideCollider;


    public Transform outsideContainer;
    public Transform insideContainer;

    [SerializeField]
    private bool isOpen = false;
    private bool hiddenContent = false;
    JointSpring spring;
    float startingAngle;
    public float angleSensibility;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(content.Count);
        if (content.Count == 0)
            this.enabled = false;
        spring = new JointSpring();
        spring.spring = 2f;
        joint.spring = spring;
        joint.useSpring = true;

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
        isOpen = joint.angle > (startingAngle + angleSensibility);
        joint.useSpring = !(joint.angle > 85f);

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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(transform.name + " Collided " + other.name);
        other.transform.parent = insideContainer;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(transform.name + " Stopped colliding " + other.name);
        other.transform.parent = outsideContainer;
    }
}
