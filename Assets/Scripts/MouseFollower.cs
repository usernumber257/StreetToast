using UnityEngine;

/// <summary>
/// gameobject attached with this follows mouse position
/// </summary>
public class MouseFollower : MonoBehaviour
{
    [Tooltip("for prevent gameobject move over the clamped positions")]
    [SerializeField] Vector2 clampMin = new Vector2(-7f, -8f);
    [SerializeField] Vector2 clampMax = new Vector2(7f, 0f);

    [Tooltip("mouse follow speed")]
    [SerializeField][Range(5f, 10f)] float followSpeed = 7f;

    Vector2 targetPos; //caching for lerp

    private void Update()
    {
        Follow();
    }

    void Follow()
    {
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);

        if (transform.position.x < clampMin.x)
            transform.position = new Vector2(clampMin.x, transform.position.y);
        else if (transform.position.x > clampMax.x)
            transform.position = new Vector2(clampMax.x, transform.position.y);

        if (transform.position.x < clampMin.y)
            transform.position = new Vector2(transform.position.x, clampMin.y);
        else if (transform.position.y > clampMax.y)
            transform.position = new Vector2(transform.position.x, clampMax.y);
    }
}
