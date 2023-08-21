using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 负责UI动画效果
/// </summary>
public static class AnimationHelper
{
    

    public static IEnumerator SlidIn(RectTransform rectTransform, Direction direction, float speed, UnityEvent OnEnd)
    {


        Vector2 startPosition = Vector2.zero;
        switch (direction)
        {
            case Direction.UP:
                startPosition = new Vector2(0, -Screen.height);
                break;
            case Direction.DOWN:
                startPosition = new Vector2(0, Screen.height);
                break;
            case Direction.LEFT:
                startPosition = new Vector2(Screen.width, 0);
                break;
            case Direction.RIGHT:
                startPosition = new Vector2(-Screen.width, 0);
                break;

        }
        float time = 0;
        while (time < 1)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, Vector2.zero, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
        rectTransform.anchoredPosition = Vector2.zero;
        OnEnd?.Invoke();

    }

    public static IEnumerator SlidOut(RectTransform rectTransform, Direction direction, float speed, UnityEvent OnEnd)
    {


        Vector2 endPosition = Vector2.zero;
        switch (direction)
        {
            case Direction.UP:
                endPosition = new Vector2(0, -Screen.height);
                break;
            case Direction.DOWN:
                endPosition = new Vector2(0, Screen.height);
                break;
            case Direction.LEFT:
                endPosition = new Vector2(Screen.width, 0);
                break;
            case Direction.RIGHT:
                endPosition = new Vector2(-Screen.width, 0);
                break;

        }
        float time = 0;
        while (time < 1)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(Vector2.zero, endPosition, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.gameObject.SetActive(false);
        OnEnd?.Invoke();

    }

    public static IEnumerator ZoomIn(RectTransform rectTransform, float speed, UnityEvent OnEnd)
    {
        float time = 0;
        while (time < 1)
        {
            rectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
        rectTransform.localScale = Vector3.one;
        OnEnd?.Invoke();
    }

    public static IEnumerator ZoomOut(RectTransform rectTransform, float speed, UnityEvent OnEnd)
    {
        float time = 0;
        while (time < 1)
        {
            rectTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
        rectTransform.localScale = Vector3.zero;
        OnEnd?.Invoke();
        rectTransform.gameObject.SetActive(false);
    }

    public static IEnumerator FadeIn(CanvasGroup canvasGroup, float speed, UnityEvent OnEnd)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        float time = 0;
        while (time < 1)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
        canvasGroup.alpha = 1;
        OnEnd?.Invoke();
    }

    public static IEnumerator FadeOut(CanvasGroup canvasGroup, float speed, UnityEvent OnEnd)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        float time = 0;
        while (time < 1)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
        canvasGroup.alpha = 0;
        OnEnd?.Invoke();
        canvasGroup.gameObject.SetActive(false);
    }




}