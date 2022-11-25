using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelVisionLookAt : MonoBehaviour
{
    public RectTransform target;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
