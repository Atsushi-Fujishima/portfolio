using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventCallFXController : MonoBehaviour
{
    public ParticleSystem[] vfxs;
    public AudioClip[] sfxs;
    public int[] randomSFXIndexs = {0, 1, 2};
    private List<AudioClip> randomSFXList = new List<AudioClip>();

    [Header("SFX")]
    [Range(0f, 1.0f)] public float soundVolume = 1.0f;
    [SerializeField] Transform playClipPoint = null;

    private void Start()
    {
        foreach (var index in randomSFXIndexs)
        {
            randomSFXList.Add(sfxs[index]);
        }
    }

    public void CallVFX()
    {
        foreach (var vfx in vfxs)
        {
            vfx.Play();
        }
    }

    public void CallSFX(int callIndex)
    {
        AudioSource.PlayClipAtPoint(sfxs[callIndex], playClipPoint.position, soundVolume);
    }

    public void CallRandomSFX()
    {
        if (randomSFXList.Count < 1) return;

        var callIndex = Random.Range(0, randomSFXList.Count);
        AudioSource.PlayClipAtPoint(randomSFXList[callIndex], playClipPoint.position, soundVolume);
    }
}
