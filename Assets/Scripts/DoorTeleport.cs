using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTeleport : MonoBehaviour
{
    [SerializeField]
    private string CollideWith;

    [SerializeField]
    private string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Detected a collision with: " + other.tag);
        if (other.CompareTag(CollideWith))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
