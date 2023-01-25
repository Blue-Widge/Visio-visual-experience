using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief Class that moves the tunnelling vignette to correspond to the camera's movement */
public class TunnelVisionLookAt : MonoBehaviour
{
    public GameObject targetObject;

    /** @brief Variable corresponding to the GUI */
    private RectTransform _target;

    /** @brief Set the GUI transform */
    private void Start()
    {
        _target = targetObject.GetComponent<RectTransform>();
    }

    /** @brief Rotate the tunnelling vignette to correspond where the user looks at */
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        if (targetObject.activeInHierarchy)
        {
            transform.LookAt(_target);
        }
    }
}
