using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultLookPlayer : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    Transform mTransform;
    public bool isLook = true;

    private void Start()
    {
        mTransform = transform;    
    }

    private void Update()
    {
        if (isLook) LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        mTransform.LookAt(new Vector3(
            playerTransform.position.x,
            mTransform.position.y,
            playerTransform.position.z));
    }

    public void DisableLookPlayer()
    {
        isLook = false;
    }
}
