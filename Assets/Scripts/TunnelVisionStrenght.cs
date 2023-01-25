using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief Class that handle the scale of the tunnel vision hole */
public class TunnelVisionStrenght : MonoBehaviour
{

    /** @brief Transform corresponding to the GUI */
    RectTransform _transform;

    [Range(1f, 10.0f)]
    public float tunnelVisionSize;


    /** @brief Set the GUI's transform */
    void Start()
    {
        _transform = GetComponent<RectTransform>();
    }

    /** @brief Set the tunnel vision hole's scale */
    void Update()
    {
        _transform.localScale = new Vector3(1,1,1) * tunnelVisionSize;
    }
}
