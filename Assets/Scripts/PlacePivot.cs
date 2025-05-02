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

    private void Update()
    {
        if (GameManager.Instance.Input.IsUp)
        {
            if (placeable == null)
                return;

            placedObjs.Add(placeable.OnPlace(transform));
            placeable = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        placeable = collision.gameObject.GetComponent<IPlaceable>();
    }
}
