using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestruction : MonoBehaviour
{
    [SerializeField] GameObject meshGameObject;
    [SerializeField] BoxCollider m_Collider;
    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] AudioClip m_Clip;

    public void WallDestroyInteraction()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(WallDestroy, 0, true));
    }

    private void WallDestroy()
    {
        meshGameObject.SetActive(false);
        m_Collider.enabled = false;
        m_AudioSource.PlayOneShot(m_Clip);
    }
}
