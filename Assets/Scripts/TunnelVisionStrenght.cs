using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelVisionStrenght : MonoBehaviour
{
    RectTransform _transform;
    [Range(1f, 10.0f)]
    public float tunnelVisionSize;

    void Start()
    {
        _transform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        _transform.localScale = new Vector3(1,1,1) * tunnelVisionSize;
    }
}
