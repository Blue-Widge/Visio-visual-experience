using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


/** @brief Script found online that simulates the wobbling of liquid inside of a bottle */
public class Wobble : MonoBehaviour
{
    Renderer _rend;
    Vector3 _lastPos;
    Vector3 _velocity;
    Vector3 _lastRot;  
    Vector3 _angularVelocity;
    [FormerlySerializedAs("MaxWobble")] public float maxWobble = 0.03f;
    [FormerlySerializedAs("WobbleSpeed")] public float wobbleSpeed = 1f;
    [FormerlySerializedAs("Recovery")] public float recovery = 1f;
    float _wobbleAmountX;
    float _wobbleAmountZ;
    float _wobbleAmountToAddX;
    float _wobbleAmountToAddZ;
    float _pulse;
    float _time = 0.5f;
    
    // Use this for initialization
    void Start()
    {
        _rend = GetComponent<Renderer>();
    }
    private void Update()
    {
        _time += Time.deltaTime;
        // decrease wobble over time
        _wobbleAmountToAddX = Mathf.Lerp(_wobbleAmountToAddX, 0, Time.deltaTime * (recovery));
        _wobbleAmountToAddZ = Mathf.Lerp(_wobbleAmountToAddZ, 0, Time.deltaTime * (recovery));

        // make a sine wave of the decreasing wobble
        _pulse = 2 * Mathf.PI * wobbleSpeed;
        _wobbleAmountX = _wobbleAmountToAddX * Mathf.Sin(_pulse * _time);
        _wobbleAmountZ = _wobbleAmountToAddZ * Mathf.Sin(_pulse * _time);

        // send it to the shader
        _rend.material.SetFloat("_WobbleX", _wobbleAmountX);
        _rend.material.SetFloat("_WobbleZ", _wobbleAmountZ);

        // velocity
        _velocity = (_lastPos - transform.position) / Time.deltaTime;
        _angularVelocity = transform.rotation.eulerAngles - _lastRot;


        // add clamped velocity to wobble
        _wobbleAmountToAddX += Mathf.Clamp((_velocity.x + (_angularVelocity.z * 0.2f)) * maxWobble, -maxWobble, maxWobble);
        _wobbleAmountToAddZ += Mathf.Clamp((_velocity.z + (_angularVelocity.x * 0.2f)) * maxWobble, -maxWobble, maxWobble);

        // keep last position
        _lastPos = transform.position;
        _lastRot = transform.rotation.eulerAngles;
    }



}