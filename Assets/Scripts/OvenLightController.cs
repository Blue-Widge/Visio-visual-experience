using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class OvenLightController : MonoBehaviour
{
    private HingeJoint hingeJoint;

    public Light ovenLight;

    public int turnOnAngle;
    // Start is called before the first frame update
    void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hingeJoint.angle >= turnOnAngle)
        {
            ovenLight.enabled = true;
        } else
        {
            ovenLight.enabled = false;
        }
        
    }
}
