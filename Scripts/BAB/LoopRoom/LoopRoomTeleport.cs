using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LoopRoomTeleport : MonoBehaviour
{
    [SerializeField] Transform playerCharacter;
    [SerializeField] Transform[] hands;
    [SerializeField] Transform targetPoint;
    public Vector3 setRotation = Vector3.zero;
    public float deleyTime = 2.0f;
    private BlinkEyeSystem eyeSystem;

    private void Start()
    {
        eyeSystem = BlinkEyeSystem.instance;
    }

    public void OnTeleport()
    {
        StartCoroutine(eyeSystem.BlinkEye(Teleport, deleyTime, true));
    }

    private void Teleport()
    {
        playerCharacter.position = targetPoint.position;
        playerCharacter.localEulerAngles = setRotation;
        foreach (Transform hand in hands)
        {
            hand.position = targetPoint.position;
        }
    }
}
