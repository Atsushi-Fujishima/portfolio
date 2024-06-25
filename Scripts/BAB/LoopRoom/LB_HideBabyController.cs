using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB_HideBabyController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] BoxCollider eventCollider;
    [SerializeField] SkinnedMeshRenderer mRenderer;
    private Transform mTransform;

    private void Start()
    {
        mTransform = transform;
    }

    private void Update()
    {
        LookPlayer();
    }

    public void DisableHideBaby()
    {
        mRenderer.enabled = false;
        eventCollider.enabled = false;
    }

    private void LookPlayer()
    {
        mTransform.LookAt(new Vector3(
            playerTransform.position.x,
            mTransform.position.y,
            playerTransform.position.z));
    }
}
