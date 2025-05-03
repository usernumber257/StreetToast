using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// instance will made when object is grabbing
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class Instance : MonoBehaviour, IPlaceable
{
    [Tooltip("visual when they placed on iron plate")]
    [SerializeField] GameObject placedVisual;

    GameObject placeInstance;
    public UnityAction OnPlaced;

    Object myObject;
    public Object MyObject { get => myObject; }

    public void Init(Object myObject)
    {
        this.myObject = myObject;
    }

    public GameObject OnPlace(Transform pivot)
    {
        placeInstance = Instantiate(placedVisual);

        placeInstance.transform.parent = pivot;
        placeInstance.transform.localPosition = Vector3.zero;

        OnPlaced?.Invoke();

        return placeInstance;
    }
}
