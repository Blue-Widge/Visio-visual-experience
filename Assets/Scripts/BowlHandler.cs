using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/** @brief Class that allows items in bowl to be children of the bowl so they fall less easily when the bowl is handled*/
public class BowlHandler : MonoBehaviour
{
    /** @brief Parent of all the items contained by the bowl */
    public Transform container;
    /** @brief Parent of all the items contained in the scene*/
    public Transform foodContainer;
    /** @brief List of all the items contained */
    [SerializeField]
    private List<GameObject> content;

    /** @brief Set the containers if they are not */
    void Start()
    {
        if (!container)
            container = transform;
        if (!foodContainer)
            foodContainer = transform.parent.transform.parent;
    }

    /** @brief If a new item is put inside the bowl, change its parent to the bowl container 
     /\param[in] other Object that entered the trigger box
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && transform.parent != other.transform)
        {
            content.Add(other.gameObject);
            other.transform.parent = container;
        }
    }

    /** @brief Unparent the bowl to the item when it goes out of it 
     /\param[in] other Object that exited the trigger box
    */
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6 && transform.parent != other.transform)
        {
            content.Remove(other.gameObject);
            other.transform.parent = foodContainer;
        }
    }
}
