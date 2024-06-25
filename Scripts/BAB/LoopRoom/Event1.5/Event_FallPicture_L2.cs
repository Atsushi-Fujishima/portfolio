using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_FallPicture_L2 : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject targetAfter;
    [SerializeField] private AudioSource mAudio;
    [SerializeField] private AudioClip mAudioClip;

    private void OnDisable()
    {
        targetAfter.SetActive(false);
        target.SetActive(true);
    }

    public void InteractionGaze()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(FallPicture, 0, true));
    }

    private void FallPicture()
    {
        mAudio.PlayOneShot(mAudioClip);
        targetAfter.SetActive(true);
        target.SetActive(false);
    }
}
