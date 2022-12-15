using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private string collideWith;
    [SerializeField]
    private string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(collideWith))
        {
            Debug.Log("Collided");
            SceneManager.LoadScene(sceneName);    
        }
    }
}
