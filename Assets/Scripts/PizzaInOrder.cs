using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

public class PizzaInOrder : MonoBehaviour
{
    public List<GameObject> ingredients = new List<GameObject>();
    public List<GameObject> pizza = new List<GameObject>();

    /// <summary>
    /// If an object collides with the cutting board check if its an ingredient, 
    /// if so check if it the next one in order, 
    /// if so delete the ingredient and activate it on the pizza.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (ingredients[0].tag == other.gameObject.tag)
        {
            ingredients.RemoveAt(0);
            Destroy(other.gameObject);

            foreach (GameObject pizzaLayer in pizza)
            {
                if (!pizzaLayer.activeSelf)
                {
                    pizzaLayer.SetActive(true);
                    return;
                }
            }
        }
    }
}
