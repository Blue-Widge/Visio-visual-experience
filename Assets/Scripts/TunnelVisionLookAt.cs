using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelVisionLookAt : MonoBehaviour
{
    public RectTransform target;
    private Vector3 targetPosLastFrame = new Vector3(0,0,0);
    // Update is called once per frame

    void FixedUpdate()
    {
        if (Mathf.Abs(target.transform.position.x - targetPosLastFrame.x) > 30)
        {
            transform.LookAt(target.position);
            targetPosLastFrame = target.transform.position;
        }
        if (Mathf.Abs(target.transform.position.y - targetPosLastFrame.y) > 30)
        {
            transform.LookAt(target.position);
            targetPosLastFrame = target.transform.position;
        }
    }
}
