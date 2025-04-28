using UnityEngine;

public class Interactor : MonoBehaviour
{
    private void Update()
    {
        Ray();
    }

    void Ray()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up);

        if (hit.collider == null)
            return;

        Debug.Log(hit.collider.gameObject);

        IInteratable interactable = hit.collider.gameObject.GetComponent<IInteratable>();

        if (interactable == null)
            return;

        interactable.OnInteract();
    }
}
