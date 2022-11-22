using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookFollowCamera : MonoBehaviour
{
    //The target object to follow
    [SerializeField]
    private Transform cameraTransform;
    //Distance that the notebook is moved forward before the camera
    [SerializeField]
    private float forwardTimes;

    void LateUpdate()
    {
        Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * forwardTimes;
        transform.position = resultingPosition;
        transform.LookAt(cameraTransform.position, cameraTransform.right);
    }
}
