using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// grabs grabbable
/// </summary>
[RequireComponent(typeof(Interactor))]
public class Grabber : MonoBehaviour
{
    [SerializeField] Transform grabPos;

    Interactor interactor;
    bool canGrab = false;

    IGrabbable grabbable; //caching

    private void Awake()
    {
        interactor = GetComponent<Interactor>();

        interactor.OnInteract += CanGrab;
    }

    private void Update()
    {
        if (!canGrab)
            return;

        if (Input.GetMouseButtonDown(0))
            Grab();
        else if (Input.GetMouseButtonUp(0))
            StopGrab();
    }

    private void OnDestroy()
    {
        interactor.OnInteract -= CanGrab;
    }

    void CanGrab()
    {
        grabbable = interactor.CurInteractableObj.GetComponent<IGrabbable>();

        if (grabbable == null)
            canGrab = false;
        else
            canGrab = true;
    }

    void Grab()
    {
        if (grabbable == null || grabPos == null)
            return;

        grabbable.OnGrab(grabPos);
    }

    void StopGrab()
    {
        if (grabbable == null || grabPos == null)
            return;

        grabbable.StopGrab();
        grabbable = null;
    }
}
