using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TablewareSoundController : MonoBehaviour
{
    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] AudioClip[] silberSounds;
    [SerializeField] AudioClip[] woodSound;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.GetComponent<ObjectSoundType>() != null)
        {
            ObjectSoundType st = other.GetComponent<ObjectSoundType>();
            if (st.soundType == ObjectSoundType.SoundType.Metal)
            {
                AudioClip clip = SelectSound(silberSounds);
                m_AudioSource.PlayOneShot(clip);
            }
            else
            {
                AudioClip clip = SelectSound(woodSound);
                m_AudioSource.PlayOneShot(clip);
            }
        }
    }

    private AudioClip SelectSound(AudioClip[] clips)
    {
        int index = Random.Range(0, clips.Length);
        return clips[index];
    }
}
