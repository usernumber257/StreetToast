using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// place placeable to it's position
/// </summary>
[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class PlacePivot : MonoBehaviour
{
    Instance curInstance; //caching
    Dictionary<MyEnum.Ingredient, int> placedIngredients = new Dictionary<MyEnum.Ingredient, int>();

    private void Update()
    {
        if (GameManager.Instance.Input.IsUp)
        {
            if (curInstance == null)
                return;

            //caching ingredient type
            MyEnum.Ingredient curIngredientType = curInstance.MyObject.IngredientType;

            if (!placedIngredients.ContainsKey(curIngredientType))
                placedIngredients.Add(curIngredientType, 0);

            //bread can place twice
            if (curIngredientType == MyEnum.Ingredient.Bread && placedIngredients[curIngredientType] >= 2)
                return;
            else if (placedIngredients[curIngredientType] >= 1) //other can place only once
                return;

            //if not placed yet, place this
            curInstance.OnPlace(transform);
            placedIngredients[curIngredientType]++; 

            curInstance = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        curInstance = collision.gameObject.GetComponent<Instance>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        curInstance = null;
    }
}
