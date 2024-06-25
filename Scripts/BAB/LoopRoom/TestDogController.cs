using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDogController : MonoBehaviour
{
    public Transform dog;
    public Transform dogMovePoint;
    public Animator dogAnimator;
    public AudioSource dogAudioSource;
    public AudioClip seDogHowl;

    public void DogWallInteraction()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(DogGoToWall, 0, true));
    }

    private void DogGoToWall()
    {
        dog.position = dogMovePoint.position;
        dog.rotation = dogMovePoint.rotation;
        dogAnimator.SetBool("Howl", true);
    }

    public void StopHowl()
    {
        dogAnimator.SetBool("Howl", false);
    }

    public void Howl()
    {
        dogAudioSource.PlayOneShot(seDogHowl);
    }
}
