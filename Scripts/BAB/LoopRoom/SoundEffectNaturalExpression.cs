using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectNaturalExpression : MonoBehaviour
{
    [SerializeField] private AudioSource mSource;
    [Header("Clips")]
    public AudioClip[] clips;
    [Header("Setting")]
    [SerializeField] private float standardPitch = 1.0f;
    [SerializeField] private float pitchRange = 0.1f;

    public void PlaySound()
    {
        mSource.pitch = standardPitch + Random.Range(-pitchRange, pitchRange);
        mSource.PlayOneShot(SelectClip());
    }

    private AudioClip SelectClip()
    {
        if (clips.Length == 1)
            return clips[0];
        else
            return clips[Random.Range(0, clips.Length)];
    }
}
