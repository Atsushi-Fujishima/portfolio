using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnEventItem : MonoBehaviour
{
    public float burnDelay = 3.0f;
    [Space]
    public GameObject fire;
    public AudioSource mAudio;
    public AudioClip mClip;
    [Header("Baby")]
    [SerializeField] GameObject baby;

    public void CallFire()
    {
        StartCoroutine(CorFire());
    }

    private IEnumerator CorFire()
    {
        yield return new WaitForSeconds(burnDelay);
        BurnFire();
        yield return new WaitForSeconds(0.5f);
        baby.SetActive(true);
        yield break;
    }

    private void BurnFire()
    {
        mAudio.PlayOneShot(mClip);
        fire.SetActive(true);
    }
}
