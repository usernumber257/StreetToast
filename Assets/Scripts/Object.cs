using System.Collections;
using UnityEngine;

/// <summary>
/// the base of default objects
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class Object : MonoBehaviour, IInteratable, IGrabbable
{
    [Header("make instance when grab")]
    [SerializeField] GameObject instancePrefab;
    [Tooltip("if it's true, hide original object when instance made")]
    [SerializeField] bool hideOrigin = false;
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
        Init();
    }

    private void Init()
    {
        sprite = GetComponent<SpriteRenderer>();

        originColor = sprite.color;
        toColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);

        originPos = transform.position;

        //use object pool for instance
        instance = Instantiate(instancePrefab);
        instance.SetActive(false);

        instance.GetComponent<Instance>().OnPlaced += StopGrab;
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
        if (instancePrefab == null)
            return;

        instance.SetActive(true);

        instance.transform.parent = grabPos;

        instance.transform.localPosition = Vector3.zero;
        instance.transform.localRotation = Quaternion.Euler(0f, 0f, grabRot);

        if (hideOrigin)
            transform.gameObject.SetActive(false);
    }

    public void StopGrab()
    {
        if (instance == null)
            return;
        
        instance.SetActive(false);

        if (hideOrigin)
            transform.gameObject.SetActive(true);
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
