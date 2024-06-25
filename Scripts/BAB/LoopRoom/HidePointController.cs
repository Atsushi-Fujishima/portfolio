using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePointController : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] BoxCollider[] thisColliders;
    [SerializeField] BoxCollider hidePointCollider;
    [SerializeField] GameObject meshGameObject;
    [SerializeField] AudioSource mAudio;
    private Transform mTransform;
    private bool isDestroy = false;

    private void Start()
    {
        mTransform = transform;
    }

    public void DestroyHidePoint()
    {
        isDestroy = true;
        meshGameObject.SetActive(false);
        foreach (var _collider in thisColliders)
        {
            _collider.enabled = false;
        }
        hidePointCollider.enabled = false;

        _particleSystem.Play();
        mAudio.Play();
    }

    public void Initialization()
    {
        isDestroy = false;
        meshGameObject.SetActive(true);
        foreach (var _collider in thisColliders)
        {
            _collider.enabled = true;
        }
        hidePointCollider.enabled = true;
    }

    public Transform GetTransform()
    {
        return mTransform;
    }

    public bool GetDestroy()
    {
        return isDestroy;
    }
}
