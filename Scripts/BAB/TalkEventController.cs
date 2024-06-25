using Prototype5;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkEventController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform setPlayerPoint;


    public void CallStartEvent()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(SetPlayerPoint, 0, false));
    }

    public void CallEndEvent()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(EndEvent, 0, true));
    }

    private void SetPlayerPoint()
    {
        playerTransform.position = new Vector3(
            setPlayerPoint.position.x,
            playerTransform.position.y,
            setPlayerPoint.position.z);
        playerTransform.rotation = setPlayerPoint.rotation;
    }

    private void EndEvent()
    {
        // use BlinkEye PlayerHandDetection true
        return;
    }
}
