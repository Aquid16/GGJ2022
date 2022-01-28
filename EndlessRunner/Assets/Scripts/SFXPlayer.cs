using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] AudioSource audioSrc;

    private void Awake()
    {
        instance = this;
        UIManager.instance.masterSlider.value = 0;
        UIManager.instance.musicSlider.value = 0;
        UIManager.instance.sfxSlider.value = 0;
    }

    [System.Serializable]
    public class SoundTypeToSound
    {
        public SFXType type;
        public AudioClip clip;
    }

    [SerializeField] List<SoundTypeToSound> soundLibrary;
    [SerializeField] AudioSource sfxSource;

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
}
