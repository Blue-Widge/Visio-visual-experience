using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable Unity.PreferAddressByIdToGraphicsParams

public class PointOrOpen : MonoBehaviour
{
    private Animator _animator;
    public bool point, openPreview;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        if (!_animator)
            this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("Point", point);
        _animator.SetBool("Open Preview",openPreview);
        
    }
}
