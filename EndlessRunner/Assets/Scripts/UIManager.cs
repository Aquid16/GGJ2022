using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] Image faderImage;
    [SerializeField] CanvasGroup deathScreen;
    [SerializeField] CanvasGroup creditsScreen;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    bool fullScreen = true;
    Vector2[] resolutions = new Vector2[] { new Vector2(1920, 1080), new Vector2(1366, 768), new Vector2(1280, 720), new Vector2(960, 540) };

    private void Start()
    {
        Fade(false, 0.5f);
        //masterSlider.value = 1;
        //musicSlider.value = 1;
        //sfxSlider.value = 1;
    }

    public void Fade(bool fadeIn, float duration = 2.5f)
    {
        Sequence fadeSequence = DOTween.Sequence();
        float fadeValue = fadeIn ? 1 : 0;
        if (fadeIn)
        {
            faderImage.gameObject.SetActive(true);
        }
        fadeSequence.Append(faderImage.DOFade(fadeValue, duration));
        if (!fadeIn)
        {
            fadeSequence.OnComplete(() => faderImage.gameObject.SetActive(false));
        }
        fadeSequence.Play();
    }

    public void DisplayDeathScreen(float duration)
    {
        Sequence fadeSequence = DOTween.Sequence();
        deathScreen.gameObject.SetActive(true);
        fadeSequence.Append(deathScreen.DOFade(1, duration)).SetDelay(1f);
        fadeSequence.Play();
    }

    public void DisplayCredits(bool isActive)
    {
        DisplayCredits(isActive, 1f);
    }

    public void DisplayCredits(bool fadeIn, float duration)
    {
        Sequence fadeSequence = DOTween.Sequence();
        float fadeValue = fadeIn ? 1 : 0;
        if(fadeIn)
        {
            creditsScreen.gameObject.SetActive(true);
        }
        fadeSequence.Append(creditsScreen.DOFade(fadeValue, duration));
        if(!fadeIn)
        {
            fadeSequence.OnComplete(() => creditsScreen.gameObject.SetActive(false));
        }
        fadeSequence.Play();

    }

    public void ChangeResolution(int index)
    {
        Screen.SetResolution((int)resolutions[index].x, (int)resolutions[index].y, fullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        fullScreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

}
