using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

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
    [SerializeField] CanvasGroup pauseScreen;
    [SerializeField] CanvasGroup settingsScreen;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI totalScoreText;

    public Slider masterSlider;
    //public Slider musicSlider;
    //public Slider sfxSlider;
    public bool subWindowOpen { get; set; }

    bool fullScreen = true;
    Vector2[] resolutions = new Vector2[] { new Vector2(1920, 1080), new Vector2(1366, 768), new Vector2(1280, 720), new Vector2(960, 540) };

    private void Start()
    {
        Fade(false, 0.5f);
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
        scoreText.gameObject.SetActive(false);
        totalScoreText.text = $"You got {ScoreHandler.instance.score} points.";
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

    public void DisplayPauseScreen(bool fadeIn, float duration)
    {
        Sequence fadeSequence = DOTween.Sequence();
        float fadeValue = fadeIn ? 1 : 0;
        if (fadeIn)
        {
            pauseScreen.gameObject.SetActive(true);
        }
        fadeSequence.Append(pauseScreen.DOFade(fadeValue, duration));
        if (!fadeIn)
        {
            fadeSequence.OnComplete(() =>
            {
                pauseScreen.gameObject.SetActive(false);
                Time.timeScale = 1f;
            });
        }
        fadeSequence.SetUpdate(true);
        fadeSequence.Play();
    }

    public void DisplaySettingsScreen(bool fadeIn)
    {
        Sequence fadeSequence = DOTween.Sequence();
        float fadeValue = fadeIn ? 1 : 0;
        if (fadeIn)
        {
            settingsScreen.gameObject.SetActive(true);
            subWindowOpen = true;
        }
        fadeSequence.Append(settingsScreen.DOFade(fadeValue, .75f));
        if (!fadeIn)
        {
            fadeSequence.OnComplete(() =>
            {
                settingsScreen.gameObject.SetActive(false);
                subWindowOpen = false;
            });
        }
        fadeSequence.SetUpdate(true);
        fadeSequence.Play();
    }

    public void EnableScoreText()
    {
        scoreText.gameObject.SetActive(true);
    }

    public void UpdateScoreText()
    {
        scoreText.text = ScoreHandler.instance.score.ToString();
    }
}
