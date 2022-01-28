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
    //[SerializeField] AudioSource sfxSource;
    //[SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource masterSource;
    [SerializeField] AudioMixer audioMxr;

    private void Start()
    {
        UIManager.instance.masterSlider.value = -35f;
        UIManager.instance.masterSlider.onValueChanged.AddListener(SetMasterVolume);

        //    UIManager.instance.musicSlider.value = 1;
        //    UIManager.instance.sfxSlider.value = 1;
        //    UIManager.instance.musicSlider.onValueChanged.AddListener(SetMusicVolume);
        //    UIManager.instance.sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

public void PlaySFX(SFXType type)
    {
        SoundTypeToSound soundData = soundLibrary.First(data => data.type == type);
        masterSource.PlayOneShot(soundData.clip);
    }

    public void PlaySFX(int index)
    {
        SoundTypeToSound soundData = soundLibrary[index];
        masterSource.PlayOneShot(soundData.clip);
    }

    public void SetMasterVolume(float value)
    {
        audioMxr.SetFloat("masterValue", value);
        masterSource.volume = value;
    }

    //public void SetMusicVolume(float value)
    //{
    //    audioMxr.SetFloat("musicValue", value);
    //    musicSource.volume = value;
    //}

    //public void SetSFXVolume(float value)
    //{
    //    audioMxr.SetFloat("sfxValue", value);
    //    sfxSource.volume = value;
    //}
}
