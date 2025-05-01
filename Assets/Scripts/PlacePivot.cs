using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// place placeable to it's position
/// </summary>
[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class PlacePivot : MonoBehaviour
{
    IPlaceable placeable; //caching
    List<GameObject> placedObjs = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        placeable = collision.gameObject.GetComponent<IPlaceable>();

        if (placeable != null && !placedObjs.Contains(collision.gameObject))
            placedObjs.Add(placeable.OnPlace(transform));
    }
}
