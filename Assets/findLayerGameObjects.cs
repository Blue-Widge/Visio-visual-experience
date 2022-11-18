using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findLayerGameObjects : MonoBehaviour
{
    GameObject[] gameObjects;
    // Start is called before the first frame update
    void Start()
    {
        gameObjects = FindObjectsOfType<GameObject>();
        foreach(GameObject gameObject in gameObjects)
        {
            if (gameObject.layer == 6)
                Debug.Log(gameObject.name); 

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
