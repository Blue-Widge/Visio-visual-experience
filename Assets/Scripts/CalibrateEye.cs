using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveSR.anipal.Eye;

public class CalibrateEye : MonoBehaviour
{

    public void CalibrateEyes()
    {
        Debug.Log("Start Calibration");
        int result = SRanipal_Eye_API.LaunchEyeCalibration(IntPtr.Zero);
        Debug.Log("Finished Calibration " + result);
    }
}
