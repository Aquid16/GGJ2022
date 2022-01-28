using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

public enum SFXType
{
    MenuClick,
    SwapToHell,
    SwapToHeaven,
    Jump,
    Death,
    HellMusic,
    HeavenMusic
}

public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer instance;
    //[SerializeField] AudioSource audioSrc;

    private void Awake()
    {
        instance = this;
    }

    [System.Serializable]
    public class SoundTypeToSound
    {
        public SFXType type;
        public AudioClip clip;
    }

    [SerializeField] List<SoundTypeToSound> soundLibrary;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource masterSource;
    [SerializeField] AudioMixer audioMxr;

    private void Start()
    {
        UIManager.instance.masterSlider.value = 1;
        UIManager.instance.musicSlider.value = 1;
        UIManager.instance.sfxSlider.value = 1;

        UIManager.instance.masterSlider.onValueChanged.AddListener(SetMasterVolume);
        UIManager.instance.musicSlider.onValueChanged.AddListener(SetMusicVolume);
        UIManager.instance.sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void PlaySFX(SFXType type)
    {
        SoundTypeToSound soundData = soundLibrary.First(data => data.type == type);
        sfxSource.PlayOneShot(soundData.clip);
    }

    public void PlaySFX(int index)
    {
        SoundTypeToSound soundData = soundLibrary[index];
        sfxSource.PlayOneShot(soundData.clip);
    }

    public void SetMasterVolume(float value)
    {
        audioMxr.SetFloat("masterValue", value);
        audioMxr.SetFloat("mainMenuMusicValue", value);
        audioMxr.SetFloat("clickSFXValue", value);
        audioMxr.SetFloat("deathSFXValue", value);
        audioMxr.SetFloat("heavenMusicValue", value);
        audioMxr.SetFloat("hellMusicValue", value);
        audioMxr.SetFloat("jumpSFXValue", value);
        audioMxr.SetFloat("swapToHellSFXValue", value);
        audioMxr.SetFloat("swapToHeavenSFXValue", value);

        masterSource.volume = value;
        musicSource.volume = value;
        sfxSource.volume = value;
    }

    public void SetMusicVolume(float value)
    {
        audioMxr.SetFloat("mainMenuMusicValue", value);
        audioMxr.SetFloat("heavenMusicValue", value);
        audioMxr.SetFloat("hellMusicValue", value);
        musicSource.volume = value;
    }

    public void SetSFXVolume(float value)
    {
        audioMxr.SetFloat("clickSFXValue", value);
        audioMxr.SetFloat("jumpSFXValue", value);
        audioMxr.SetFloat("swapToHellSFXValue", value);
        audioMxr.SetFloat("swapToHeavenSFXValue", value);
        audioMxr.SetFloat("deathSFXValue", value);
        sfxSource.volume = value;
    }
}
