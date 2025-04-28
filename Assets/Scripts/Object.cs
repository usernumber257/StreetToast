using System.Collections;
using UnityEngine;

public class Object : MonoBehaviour, IInteratable
{
    [SerializeField] float duringTime = 2f;
    [SerializeField] float speed = 1f;

    SpriteRenderer sprite;
    Color toColor;
    Color originColor;
    Color curColor;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        originColor = sprite.color;
        toColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);
    }

    public void OnInteract()
    {
        Debug.Log(gameObject.name);
        Blink();
    }

    void Blink()
    {
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
