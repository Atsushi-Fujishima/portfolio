using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVolumeChanger : MonoBehaviour
{
    [SerializeField] AudioSource mAudio;
    [Header("Volume")]
    [SerializeField] float highVolume;
    [SerializeField] float lowVolume;
    [SerializeField] float volumeRange = 0.1f;
    [SerializeField] float volumeChangeValue = 0.1f;
    private float defaultVolume = 1.0f;

    private void Start()
    {
        defaultVolume = mAudio.volume;
    }

    public void SetDefaultVolume()
    {
        mAudio.volume = defaultVolume;
    }

    public void SetLowVolume()
    {
        mAudio.volume = lowVolume + Random.Range(volumeRange-volumeRange, volumeRange);
    }

    public void SetHighVolume()
    {
        mAudio.volume = highVolume + Random.Range(volumeRange - volumeRange, volumeRange);
    }

    public void FadeOutVolume()
    {
        StartCoroutine(FadeOutCountrol());
    }

    public void FadeInVolume()
    {
        StartCoroutine(FadeInControl());
    }

    private IEnumerator FadeOutCountrol()
    {
        while (mAudio.volume > 0.01f)
        {
            mAudio.volume -= volumeChangeValue * Time.deltaTime;
            yield return null;
        }

        mAudio.volume = 0;
        yield break;
    }

    private IEnumerator FadeInControl()
    {
        while (mAudio.volume < defaultVolume)
        {
            mAudio.volume += volumeChangeValue * Time.deltaTime;
            yield return null;
        }

        mAudio.volume = defaultVolume;
        yield break;
    }
}
