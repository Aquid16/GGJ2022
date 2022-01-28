using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicAudioSource : MonoBehaviour
{
    PlayerActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerActions();

        inputActions.Gameplay.Swap.performed += ctx => CrossFade();
    }

    [SerializeField] AudioClip loopedClip;
    [SerializeField] float maxVolume;
    bool shouldBeActive;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        shouldBeActive = source.volume > 0;
        StartCoroutine(CheckForSwitch());
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    IEnumerator CheckForSwitch()
    {
        while (source.isPlaying)
        {
            yield return null;
        }
        source.clip = loopedClip;
        source.loop = true;
        source.Play();
    }

    void CrossFade()
    {
        shouldBeActive = !shouldBeActive;
        float targetVolume = shouldBeActive ? maxVolume : 0;
        source.DOFade(targetVolume, 0.75f);
    }
}
