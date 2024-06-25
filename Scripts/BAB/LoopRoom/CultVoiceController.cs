using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultVoiceController : MonoBehaviour
{
    public bool isOneced = true;
    public bool isAwake = true;
    [Space]
    [SerializeField] AudioSource mAudioSource;
    [SerializeField] AudioClip voiceClip;
    [SerializeField] float voiceDelayMax = 2.0f;
    [SerializeField] float voiceDelayMin = 1.0f;
    private bool isPlay = false;
    private float playAudioDelay = 0f;
    private float playVoiceDelayElapsed = 0f;

    private void Start()
    {
        isPlay = isAwake;

        mAudioSource.clip = voiceClip;
        mAudioSource.loop = false;
        playAudioDelay = 0f;
    }

    private void Update()
    {
        if (isPlay) VoiceControl();
    }

    private void VoiceControl()
    {
        if (mAudioSource.isPlaying == false)
        {
            playVoiceDelayElapsed += Time.deltaTime;
            if (playVoiceDelayElapsed > playAudioDelay)
            {
                playVoiceDelayElapsed = 0f;
                SetDelayTime();
                mAudioSource.Play();

                if (isOneced)
                {
                    isPlay = false;
                }
            }
        }
    }

    private void SetDelayTime()
    {
        playAudioDelay = Random.Range(voiceDelayMin, voiceDelayMax);
    }

    public void PlayVoiceSound()
    {
        isPlay = true;
    }

    public void StopVoiceSound()
    {
        isPlay = false;
    }
}
