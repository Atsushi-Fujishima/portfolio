using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPedestalController : MonoBehaviour
{
    [SerializeField] GameObject keyCube;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;

    public void PedestalInteraction()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(PedestalControl, 0f, true));
    }

    private void PedestalControl()
    {
        keyCube.SetActive(true);
        audioSource.PlayOneShot(clip);
    }
}
