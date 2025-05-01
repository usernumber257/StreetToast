using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// instance will made when object is grabbing
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class Instance : MonoBehaviour, IPlaceable
{
    GameObject placeInstance;
    public UnityAction OnPlaced;

    public GameObject OnPlace(Transform pivot)
    {
        placeInstance = Instantiate(gameObject);

        placeInstance.transform.parent = pivot;
        placeInstance.transform.localPosition = Vector3.zero;

        OnPlaced?.Invoke();

        return placeInstance;
    }
}
