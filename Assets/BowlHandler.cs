using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlHandler : MonoBehaviour
{
    public Transform container;
    public Transform foodContainer;
    [SerializeField]
    private List<GameObject> content;
    // Start is called before the first frame update
    void Start()
    {
        if (!container)
            container = transform;
        if (!foodContainer)
            foodContainer = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            content.Add(other.gameObject);
            other.transform.parent = container;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer ==6 && other.transform.parent != container)
            other.transform.parent = container;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            content.Remove(other.gameObject);
            other.transform.parent = foodContainer;
        }
    }
}
