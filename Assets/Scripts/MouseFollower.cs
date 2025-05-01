using System.Collections;
using UnityEngine;

/// <summary>
/// gameobject attached with this follows mouse position
/// </summary>
public class MouseFollower : MonoBehaviour
{
    [Header("Mouse Follow")]
    [SerializeField] Vector2 clampMin = new Vector2(-7.5f, -8f);
    [SerializeField] Vector2 clampMax = new Vector2(7.5f, 1f);

    [SerializeField][Range(5f, 10f)] float followSpeed = 7f;

    [Header("Flip by its position")]
    [SerializeField] bool canFlip = true;

    [Header("Hand Down Animation")]
    [SerializeField] bool downAnim = true;
    [SerializeField] Transform modelParent;
    [SerializeField][Range(0.01f, 0.1f)] float downSpeed = 0.03f;
    float downAmount = -6f;

    [Header("do not flip when grab object")]
    [Tooltip("if it's null, flips when grabbing object")]
    [SerializeField] Grabber grabber;

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


        //flips gameObject by mouse position on screen when canFilp true
        if (!canFlip)
            return;

        //can't flip when grabbing something
        if (grabber != null && grabber.IsGrabbing)
            return;

        if (targetPos.x < 0 && transform.localScale.x != -1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            HandDown();
        }
        else if (targetPos.x > 0 && transform.localScale.x != 1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            HandDown();
        }
    }

    /// <summary>
    /// starts hand down with animation
    /// </summary>
    void HandDown()
    {
        if (handDownRoutine != null)
            StopCoroutine(handDownRoutine);

        handDownRoutine = StartCoroutine(HandDownTime());
    }

    Coroutine handDownRoutine;
    IEnumerator HandDownTime()
    {
        while (true)
        {
            if (modelParent.localPosition.y <= downAmount)
                break;

            modelParent.localPosition = Vector2.Lerp(modelParent.localPosition, new Vector2(modelParent.localPosition.x, downAmount - 0.1f), downSpeed);
            yield return null;
        }

        while (true)
        {
            if (modelParent.localPosition.y >= targetPos.y)
                break;

            //if mouse pos on screen over clampMax.y, hand will move to clampMax.y
            if (targetPos.y < clampMax.y)
                modelParent.localPosition = Vector2.Lerp(modelParent.localPosition, new Vector2(modelParent.localPosition.x, targetPos.y + 0.1f), downSpeed);
            else
                modelParent.localPosition = Vector2.Lerp(modelParent.localPosition, new Vector2(modelParent.localPosition.x, clampMax.y + 0.1f), downSpeed);

            yield return null;
        }

        modelParent.localPosition = Vector2.zero;
    }
}
