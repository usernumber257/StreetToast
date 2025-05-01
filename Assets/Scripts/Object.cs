using System.Collections;
using UnityEngine;

/// <summary>
/// the base of default objects
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class Object : MonoBehaviour, IInteratable, IGrabbable
{
    [Header("make instance when grab")]
    [Tooltip("if it's null, they dont make instance")]
    [SerializeField] GameObject instancePrefab;
    GameObject instance;

    [Header("anim blinks when interaction")]
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

    public void OnGrab(Transform grabPos, float grabRot = 0f)
    {
        Debug.Log($"on grab {gameObject.name}");
        if (instancePrefab == null)
        {
            transform.parent = grabPos;

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(0f, 0f, grabRot);
        }
        else
        {
            //use object pool for instance
            if (instance == null)
                instance = Instantiate(instancePrefab);
            
            instance.SetActive(true);

            instance.transform.parent = grabPos;

            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.Euler(0f, 0f, grabRot);
        }
    }

    public void StopGrab()
    {
        Debug.Log($"stop grab {gameObject.name}");
        if (instance == null)
        {
            transform.parent = null;

            transform.position = originPos;
            transform.rotation = Quaternion.identity;
        }
        else
            instance.SetActive(false);
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
