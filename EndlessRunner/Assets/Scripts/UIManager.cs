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
}
