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
    Death
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
    [SerializeField] AudioMixer audioMxr;

    private void Start()
    {
        UIManager.instance.masterSlider.value = 0;
        UIManager.instance.musicSlider.value = 0;
        UIManager.instance.sfxSlider.value = 0;
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
    }

    public void SetMusicVolume(float value)
    {
        audioMxr.SetFloat("musicValue", value);
    }

    public void SetSFXVolume(float value)
    {
        audioMxr.SetFloat("sfxValue", value);
    }
}
