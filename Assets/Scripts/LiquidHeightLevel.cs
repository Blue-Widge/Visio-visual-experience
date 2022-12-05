using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LiquidHeightLevel : MonoBehaviour
{
    private GameObject _bottle;
    private Renderer _renderer;
    private float _bottleHeight;
    private float _minHeight;
    private Vector3 _rotation;
    
    private static readonly int _level = Shader.PropertyToID("_Level");

    // Start is called before the first frame update
    void Start()
    {
        _bottle = transform.parent.gameObject;
        _renderer = GetComponent<Renderer>();
        if (!_bottle || !_renderer)
            this.enabled = false;
        _bottleHeight = _minHeight = _renderer.material.GetFloat(_level);
    }

    private void Update()
    {
        float rotationVectorX = 2.0f - Vector3.Distance(_bottle.transform.up, Vector3.down);
        _bottleHeight = Mathf.Lerp(_minHeight, _minHeight + 0.05f, rotationVectorX / 2.0f);
        _renderer.material.SetFloat(_level, _bottleHeight); 
    }
}
