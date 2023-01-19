using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaInOrder : MonoBehaviour
{
    public List<GameObject> ingredients = new List<GameObject>();
    public List<GameObject> pizza = new List<GameObject>();

    private int _ingredientsDone;
    //to disable the right preview
    public int id;
    public Material recipeMaterial;
    private static readonly int PizzaIngredientsDone = Shader.PropertyToID("_PizzaIngredientsDone");

    /// <summary>   
    /// If an object collides with the cutting board check if its an ingredient, 
    /// if so check if it the next one in order, 
    /// if so delete the ingredient and activate it on the pizza.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (ingredients.Contains(collision.gameObject))
        {
            if (ingredients.IndexOf(collision.gameObject) == 0)
            {
                _ingredientsDone++;
                recipeMaterial.SetFloat(PizzaIngredientsDone, _ingredientsDone);
                ingredients.RemoveAt(0);
                Destroy(collision.gameObject);
                EventSystemHandler.current.CuttingBoardUsed(id);
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
}
