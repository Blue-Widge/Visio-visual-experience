using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @brief Class that handle the animation of the hand's preview to point or open */
public class PointOrOpen : MonoBehaviour
{
    private Animator _animator;
    public bool point, openPreview;
    /** @brief Set the animator and disable the script if none has been found */
    void Start()
    {
        _animator = GetComponent<Animator>();
        if (!_animator)
            this.enabled = false;
    }

    /** @brief Change the animator animation by sending the booleans value to it*/
    void Update()
    {
        _animator.SetBool("Point", point);
        _animator.SetBool("Open Preview",openPreview);
        
    }
}
