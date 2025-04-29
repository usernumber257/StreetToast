using System.Collections;
using UnityEngine;

/// <summary>
/// the base of default objects
/// </summary>
public class Object : MonoBehaviour, IInteratable, IGrabbable
{
    [SerializeField] float duringTime = 2f;
    [SerializeField] float speed = 0.01f;

    SpriteRenderer sprite;
    Color toColor;
    Color originColor;
    Color curColor;

    Vector2 originPos;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        originColor = sprite.color;
        toColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);

        originPos = transform.position;
    }

    public void OnInteract()
    {
        Blink();
    }

    public void StopInteract()
    {
        if (blinkRoutine != null)
        {
            StopCoroutine(blinkRoutine);
            blinkRoutine = null;

            sprite.color = originColor;
        }
    }

    public void OnGrab(Transform grabPos)
    {
        transform.parent = grabPos;
        transform.localPosition = Vector3.zero;
    }

    public void StopGrab()
    {
        transform.parent = null;
        transform.position = originPos;
    }

    void Blink()
    {
        if (blinkRoutine == null)
            blinkRoutine = StartCoroutine(BlinkTime());
    }

    Coroutine blinkRoutine;
    IEnumerator BlinkTime()
    {
        float progress = 0f;
        bool hover = true;

        while (true)
        {
            if (hover)
            {
                if (progress < duringTime)
                {
                    curColor = Color.Lerp(originColor, toColor, progress);
                    sprite.color = curColor;
                    progress += speed;
                    yield return null;
                }
                else
                {
                    hover = false;
                    progress = 0f;
                }
            }
            else
            {
                if (progress < duringTime)
                {
                    curColor = Color.Lerp(toColor, originColor, progress);
                    sprite.color = curColor;
                    progress += speed;
                    yield return null;
                }
                else
                {
                    hover = true;
                    progress = 0f;
                }
            }
        }
    }

}
