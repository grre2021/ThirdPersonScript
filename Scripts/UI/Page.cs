using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UI页面脚本，负责管理此页面的动画,音频效果
/// </summary>

[RequireComponent(typeof(AudioSource), typeof(CanvasGroup))]
[DisallowMultipleComponent]
public class Page : MonoBehaviour
{
    private AudioSource audioSource;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField]
    private float animationSpeed = 1f;

    public bool exitOnNewPagePush = false;

    [SerializeField]
    private AudioClip entryClip;
    [SerializeField]
    private AudioClip exitClip;
    [SerializeField]
    private EntryMode entryMode = EntryMode.SLIDE;
    [SerializeField]
    private Direction entryDirection = Direction.LEFT;
    [SerializeField]
    private EntryMode exitMode = EntryMode.SLIDE;
    [SerializeField]
    private Direction exitDirection = Direction.LEFT;

    private Coroutine AnimationCoroutine;
    private Coroutine AudioCoroutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0;



    }
    /// <summary>
    /// 进入效果
    /// </summary>
    /// <param name="playAudio"></param>
    public void Entry(bool playAudio)
    {
        switch (entryMode)
        {
            case EntryMode.SLIDE:
                SlideIn(playAudio);
                break;
            case EntryMode.ZOOM:
                ZoomIn(playAudio);
                break;
            case EntryMode.FADE:
                FadeIn(playAudio);
                break;

        }
    }
    /// <summary>
    /// 退出效果
    /// </summary>
    /// <param name="playAudio"></param>

    public void Exit(bool playAudio)
    {
        switch (exitMode)
        {
            case EntryMode.SLIDE:
                SlideOut(playAudio);
                break;
            case EntryMode.ZOOM:
                ZoomOut(playAudio);
                break;
            case EntryMode.FADE:
                FadeOut(playAudio);
                break;

        }
    }
    /// <summary>
    /// 滑入
    /// </summary>
    /// <param name="playAudio"></param>
    public void SlideIn(bool playAudio)
    {
        if (AnimationCoroutine != null)
        {
            StopCoroutine(AnimationCoroutine);
        }
        //使用协程来完成动画效果
        AnimationCoroutine = StartCoroutine(AnimationHelper.SlidIn(rectTransform, entryDirection, animationSpeed, null));
        PlayEnterClip(playAudio);
    }
    /// <summary>
    /// 滑出
    /// </summary>
    /// <param name="playAudio"></param>
    public void SlideOut(bool playAudio)
    {
        if (AnimationCoroutine != null)
        {
            StopCoroutine(AnimationCoroutine);
        }
        AnimationCoroutine = StartCoroutine(AnimationHelper.SlidOut(rectTransform, exitDirection, animationSpeed, null));
        PlayExitClip(playAudio);
    }
    /// <summary>
    /// 缩放
    /// </summary>
    /// <param name="playAudio"></param>
    public void ZoomIn(bool playAudio)
    {
        if (AnimationCoroutine != null)
        {
            StopCoroutine(AnimationCoroutine);
        }

        AnimationCoroutine = StartCoroutine(AnimationHelper.ZoomIn(rectTransform, animationSpeed, null));
        PlayEnterClip(playAudio);
    }

    public void ZoomOut(bool playAudio)
    {
        if (AnimationCoroutine != null)
        {
            StopCoroutine(AnimationCoroutine);
        }
        AnimationCoroutine = StartCoroutine(AnimationHelper.ZoomOut(rectTransform, animationSpeed, null));
        PlayExitClip(playAudio);
    }
    /// <summary>
    /// 淡入
    /// </summary>
    /// <param name="playAudio"></param>
    public void FadeIn(bool playAudio)
    {
        if (AnimationCoroutine != null)
        {
            StopCoroutine(AnimationCoroutine);
        }
        AnimationCoroutine = StartCoroutine(AnimationHelper.FadeIn(canvasGroup, animationSpeed, null));
        PlayEnterClip(playAudio);
    }

    public void FadeOut(bool playAudio)
    {
        if (AnimationCoroutine != null)
        {
            StopCoroutine(AnimationCoroutine);
        }
        AnimationCoroutine = StartCoroutine(AnimationHelper.FadeOut(canvasGroup, animationSpeed, null));
        PlayExitClip(playAudio);
    }

/// <summary>
/// 进入音效
/// </summary>
/// <param name="playAudio"></param>
    private void PlayEnterClip(bool playAudio)
    {
        if (playAudio && entryClip != null && audioSource != null)
        {
            if (AudioCoroutine != null)
            {
                StopCoroutine(AudioCoroutine);
            }
        }
        if (entryClip == null) return;
        AudioCoroutine = StartCoroutine(PlayClip(entryClip));
    }

    /// <summary>
    /// 退出音效
    /// </summary>
    /// <param name="playAudio"></param>
    private void PlayExitClip(bool playAudio)
    {
        if (playAudio && entryClip != null && audioSource != null)
        {
            if (AudioCoroutine != null)
            {
                StopCoroutine(AudioCoroutine);
            }
        }
        if (entryClip == null) return;
        AudioCoroutine = StartCoroutine(PlayClip(exitClip));
    }
    private IEnumerator PlayClip(AudioClip clip)
    {
        audioSource.enabled = true;
        WaitForSeconds waitForSeconds = new WaitForSeconds(clip.length);
        audioSource.PlayOneShot(clip);
        yield return waitForSeconds;
        audioSource.enabled = false;
    }
}


public enum EntryMode
{
    DO_NOTHING,
    SLIDE,
    ZOOM,
    FADE
}
public enum Direction
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

