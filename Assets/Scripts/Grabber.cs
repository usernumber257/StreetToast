using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// grabs grabbable
/// </summary>
[RequireComponent(typeof(Interactor))]
public class Grabber : MonoBehaviour
{
    [SerializeField] Transform grabPos;
    [SerializeField] float grabRot = 50f;

    Interactor interactor;
    bool canGrab = false;

    IGrabbable grabbable; //caching

    bool isGrabbing;
    public bool IsGrabbing { get { return isGrabbing; } }

    public UnityAction OnStopGrab;

    private void Awake()
    {
        interactor = GetComponent<Interactor>();

        interactor.OnInteract += CanGrab;
    }

    private void Update()
    {
        if (GameManager.Instance.Input.IsDown)
        {
            if (canGrab && !isGrabbing)
                Grab();
        }
        else
        {
            if (!canGrab || isGrabbing)
                StopGrab();
        }
    }

    private void OnDestroy()
    {
        interactor.OnInteract -= CanGrab;
    }

    void CanGrab(bool canGrab)
    {
        if (grabbable == null)
        {
            this.canGrab = false;
            grabbable = interactor.CurInteractableObj.GetComponent<IGrabbable>();
        }
        else
            this.canGrab = true;
    }

    void Grab()
    {
        if (grabbable == null || grabPos == null)
            return;

        grabbable.OnGrab(grabPos);
        isGrabbing = true;
    }

    void StopGrab()
    {
        if (grabbable == null || grabPos == null)
            return;

        grabbable.StopGrab();
        grabbable = null;

        OnStopGrab?.Invoke();

        isGrabbing = false;
    }
}
