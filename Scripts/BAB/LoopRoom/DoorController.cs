using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] Animator mAnimator;
    [SerializeField] AudioSource mAudio;
    [SerializeField] AudioClip sfxOpen;

    public void OpenDoor()
    {
        mAnimator.SetBool("Open", true);
        mAudio.PlayOneShot(sfxOpen);
    }

    public void CloseDoor()
    {
        mAnimator.SetBool("Open", false);
    }
}
