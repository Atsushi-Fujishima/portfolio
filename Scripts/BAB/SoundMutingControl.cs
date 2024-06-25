using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMutingControl : MonoBehaviour
{
    public bool isEnable = true;
    [SerializeField] AudioSource[] audios;
    private List<GameObject> audioGameObjects = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < audios.Length; i++)
        {
            audioGameObjects.Add(audios[i].gameObject);
        }
    }

    public void AudioMuting()
    {
        if (isEnable == false)
            return;

        if (IsSystemBreak()) 
            return;

        StartCoroutine(MutingControl());
    }

    private IEnumerator MutingControl()
    {
        for (int i = 0; i < audios.Length; i++)
        {
            if (IsActiveGameObject(audioGameObjects[i]))
            {
                audios[i].volume = 0f;
            }
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < audios.Length; i++)
        {
            if (IsActiveGameObject(audioGameObjects[i]))
            {
                if (audios[i].isPlaying)
                {
                    audios[i].Stop();
                }
            }
        }

        yield break;
    }

    private bool IsActiveGameObject(GameObject target)
    {
        if (target.activeSelf)
            return true;
        else
            return false;
    }

    private bool IsSystemBreak()
    {
        foreach (var target in audioGameObjects)
        {
            if (target.activeSelf)
                return false;
        }

        return true;
    }
}
