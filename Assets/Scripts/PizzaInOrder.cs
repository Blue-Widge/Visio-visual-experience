using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/**@brief Class that handles the pizza mini-game */
public class PizzaInOrder : MonoBehaviour
{
    /** @brief List of all the ingredients needed */
    public List<GameObject> ingredients = new List<GameObject>();
    /** @brief List of all the mesh of the pizza, representing the steps */
    public List<GameObject> pizza = new List<GameObject>();
    /** @brief XRGrabInteractable component of the pizza gameobject*/
    public XRGrabInteractable pizzaObject;
    /** @brief Number of ingredients done, to send to the recipe material */
    private int _ingredientsDone;
    /** @brief id of the cutting board hand preview */
    public int id;
    /** @brief The material of the recipe list on the fridge */
    public Material recipeMaterial;
    private static readonly int PizzaIngredientsDone = Shader.PropertyToID("_PizzaIngredientsDone");

    /** @brief Set the number of ingredients done to 0 on the recipe list's material*/
    private void Start()
    {
        _ingredientsDone = 0;
        recipeMaterial.SetFloat(PizzaIngredientsDone, _ingredientsDone);
    }

    /// <summary>   
    /// If an object collides with the cutting board check if its an ingredient, 
    /// if so check if it the next one in order, 
    /// if so delete the ingredient and activate it on the pizza.
    /// </summary>
    /// <param name="other"></param>

/** @brief Function that compares the tag of the gameobject needed and the handled one to know if it's the right ingredients
 *  or not. If so, it will disable the gameobject, unhide one step of the pizza and increase the number of ingredients done.
 *  The pizza will be grabbable once the number of ingredients is less than 4 
 *\param[in] other The object that entered in the trigger box
 */
private void OnTriggerEnter(Collider other)
{
    if (!ingredients[0].CompareTag(other.gameObject.tag)) return;
    
    _ingredientsDone++;
    recipeMaterial.SetFloat(PizzaIngredientsDone, _ingredientsDone);
            
    ingredients.RemoveAt(0);
    Destroy(other.gameObject);
    EventSystemHandler.Current.CuttingBoardUsed(id);
            
    if (ingredients.Count < 4)
        pizzaObject.interactionLayers = 1;

    foreach (var pizzaLayer in pizza)
    {
        if (pizzaLayer.activeSelf) continue;
        
        pizzaLayer.SetActive(true);
        return;
    }
}
}
