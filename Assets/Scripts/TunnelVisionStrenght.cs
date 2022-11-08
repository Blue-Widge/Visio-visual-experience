using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelVisionStrenght : MonoBehaviour
{
    RectTransform transform;
    [Range(1f, 10.0f)]
    public float tunnelVisionSize;

    void Start()
    {
        transform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1,1,1) * tunnelVisionSize;
    }
}
