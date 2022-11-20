using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii;
using Tobii.XR;
using UnityEngine.XR.OpenXR.Features.Interactions;

public class EyeTrackingTest : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.Local);
        Vector3 newPos = new Vector3(eyeTrackingData.GazeRay.Direction.x, eyeTrackingData.GazeRay.Direction.y, 0);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(newPos);
        }
    }
}
