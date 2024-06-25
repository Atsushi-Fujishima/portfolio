using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhisperController : MonoBehaviour
{
    [SerializeField] AudioSource mAudio;
    public float decValue = 0.8f;

    public void CallStopWhisper()
    {
        StartCoroutine(StopWhisper());
    }

    private IEnumerator StopWhisper()
    {
        while (mAudio.volume > 0.01f)
        {
            mAudio.volume -= 0.5f * Time.deltaTime;
            yield return null;
        }

        mAudio.volume = 0f;
        mAudio.loop = false;
        mAudio.Stop();
        yield break;
    }
}
