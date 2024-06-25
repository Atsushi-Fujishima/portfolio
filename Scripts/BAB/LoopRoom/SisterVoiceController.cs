using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SisterVoiceController : MonoBehaviour
{
    [Header("Soliloquy Sound")]
    [SerializeField] AudioSource soliAudio;
    [SerializeField] SoundVolumeChanger soliSoundVolumeChanger;
    [SerializeField] AudioClip soliloquyClip;
    [SerializeField] float soliDelayMax = 3.0f;
    [SerializeField] float soliDelayMin = 2.0f;
    private bool isPlaySoundSoli = true;
    private float playSoliloquyDelay = 0;
    private float soliDelayElapsedTime = 0;
    [Header("Anger Sound")]
    [SerializeField] AudioSource angerAudio;
    [SerializeField] AudioClip angerClip;
    [SerializeField] float angerDefaultVolume = 1.0f;
    [SerializeField] float initializeTime = 2.0f;
    private float angerClipLength = 0f;
    [Header("Find Sound")]
    [SerializeField] AudioSource findAudio;
    [SerializeField] AudioClip findClip;
    [SerializeField] float findDefaultVolume = 1.0f;
    [SerializeField] float initializeFindTime = 1f;
    private float findClipLength = 0f;

    private bool isStarted = false;

    private void Start()
    {
        if (angerClip != null) angerClipLength = angerClip.length;
        if (findClip != null) findClipLength = findClip.length;
        soliAudio.clip = soliloquyClip;
        soliAudio.loop = false;
        soliAudio.Play();
        SetSoliloquyDelayTime();
        isStarted = true;
    }

    private void OnEnable()
    {
        if (isStarted == false) return;
        InitializeVoiceSound();
    }

    private void Update()
    {
        if (isPlaySoundSoli) SoliloquyControl();
    }

    private void SoliloquyControl()
    {
        if (soliAudio.isPlaying == false)
        {
            soliDelayElapsedTime += Time.deltaTime;
            if (soliDelayElapsedTime > playSoliloquyDelay)
            {
                soliDelayElapsedTime = 0f;
                SetSoliloquyDelayTime();
                soliAudio.Play();
            }
        }
    }

    public void StopSoliSoundControl()
    {
        isPlaySoundSoli = false;
        soliSoundVolumeChanger.FadeOutVolume();
    }

    public void PlaySoundSoli()
    {
        soliSoundVolumeChanger.FadeInVolume();
        soliAudio.Play();
        soliDelayElapsedTime = 0f;
        SetSoliloquyDelayTime();
        isPlaySoundSoli = true;
    }

    private void SetSoliloquyDelayTime()
    {
        playSoliloquyDelay = Random.Range(soliDelayMin, soliDelayMax);
    }

    public void PlaySoundAnger()
    {
        StartCoroutine(AngerSoundControl());
    }

    private IEnumerator AngerSoundControl()
    {
        StopSoliSoundControl();
        soliSoundVolumeChanger.FadeOutVolume();
        yield return new WaitForSeconds(0.1f);
        angerAudio.volume = angerDefaultVolume;
        angerAudio.PlayOneShot(angerClip);
        yield return new WaitForSeconds(angerClipLength);
        yield return new WaitForSeconds(initializeTime);
        soliSoundVolumeChanger.FadeInVolume();
        PlaySoundSoli();
        yield break;
    }

    public void PlaySoundFind()
    {
        StartCoroutine(FindSoundControl());
    }

    private IEnumerator FindSoundControl()
    {
        StopSoliSoundControl();
        soliSoundVolumeChanger.FadeOutVolume();
        yield return new WaitForSeconds(0.1f);
        findAudio.volume = findDefaultVolume;
        findAudio.PlayOneShot(findClip);
        yield return new WaitForSeconds(findClipLength);
        yield return new WaitForSeconds(initializeFindTime);
        soliSoundVolumeChanger.FadeInVolume();
        PlaySoundSoli();
        yield break;
    }

    private void InitializeVoiceSound()
    {
        if (isPlaySoundSoli)
        {
            soliAudio.volume = 0.01f;
            PlaySoundSoli();
        }
        else
        {
            angerAudio.volume = 0.01f;
            findAudio.volume = 0.01f;
            PlaySoundSoli();
        }
    }
}
