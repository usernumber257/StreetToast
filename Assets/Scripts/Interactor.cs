using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// interactor interacts with interactable
/// </summary>
public class Interactor : MonoBehaviour
{
    IInteratable prevInteractable;
    GameObject curInteractableObj;
    public GameObject CurInteractableObj { get { return curInteractableObj; } }

    public UnityAction<bool> OnInteract;

    private void Update()
    {
        Ray();
    }

    void Ray()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up);

        if (hit.collider == null)
        {
            if (prevInteractable != null)
            {
                prevInteractable.StopInteract();

                OnInteract?.Invoke(false);
                prevInteractable = null;
            }

            return;
        }

        IInteratable interactable = hit.collider.gameObject.GetComponent<IInteratable>();

        if (interactable == null)
            return;

        curInteractableObj = hit.collider.gameObject;
        OnInteract?.Invoke(true);

        if (prevInteractable != null && prevInteractable != interactable)
        {
            prevInteractable.StopInteract();
            OnInteract?.Invoke(false);
        }

        interactable.OnInteract();

        prevInteractable = interactable; //caching for later interaction
    }
}
