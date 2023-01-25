using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** @brief Class that changes the scene when the player hit the hallway's door */
public class ChangeScene : MonoBehaviour
{
    /** @brief Contains "Hand" in the editor, so it changes when the hand hit the door */
    [SerializeField]
    private string collideWith;

    /** @brief Name of the scene where the user wants to go */
    [SerializeField]
    private string sceneName;


    /** @brief Function that changes the scene when an object enters the door collider */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(collideWith))
        {
            Debug.Log("Collided");
            SceneManager.LoadScene(sceneName);    
        }
    }
}
