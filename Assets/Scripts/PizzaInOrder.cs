using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PizzaInOrder : MonoBehaviour
{
    public List<GameObject> ingredients = new List<GameObject>();
    public List<GameObject> pizza = new List<GameObject>();
    public XRGrabInteractable pizzaObject;

    private int _ingredientsDone;
    //to disable the right preview
    public int id;
    public Material recipeMaterial;
    private static readonly int PizzaIngredientsDone = Shader.PropertyToID("_PizzaIngredientsDone");

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
