using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VrLocomotionController : MonoBehaviour
{
    [SerializeField]
    public float speed = 1.0f;
    private Vector2 Axis;

    [SerializeField]
    private Transform cameraTransform;

    // Update is called once per frame
    void Update()
    {
        Axis = SteamVR_Actions._default.TouchpadPosition.axis;
        if (Axis != null)
        {
            transform.position += (Axis.x * cameraTransform.right * speed + Axis.y * cameraTransform.forward * speed) * Time.deltaTime;
            transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
        }
    } 
}
