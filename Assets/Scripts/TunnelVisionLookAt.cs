using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelVisionLookAt : MonoBehaviour
{
    public GameObject targetObject;
    private RectTransform target;

    private void Start()
    {
        target = targetObject.GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        if (targetObject.activeInHierarchy)
        {
            transform.LookAt(target);
        }
    }
}
