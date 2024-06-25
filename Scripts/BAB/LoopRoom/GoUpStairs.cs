using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoUpStairs : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform[] handTransforms;
    [SerializeField] Transform m_TeleportTarget;
    public Vector3 setRotation = Vector3.one;

    public void GoUpTeleport()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(GoUp, 0, true));
    }

    private void GoUp()
    {
        playerTransform.position = m_TeleportTarget.position;
        playerTransform.localEulerAngles = setRotation;
        foreach (Transform hand in handTransforms)
        {
            hand.position = m_TeleportTarget.position;
        }
    }
}
