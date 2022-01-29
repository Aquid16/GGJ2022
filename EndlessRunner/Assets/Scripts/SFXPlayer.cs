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
    [SerializeField] AudioSource masterSource;

    private void Start()
    {
        UIManager.instance.masterSlider.onValueChanged.AddListener(SetMasterVolume);
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
        AudioListener.volume = value;
    }
}
