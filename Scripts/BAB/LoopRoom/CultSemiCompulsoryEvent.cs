using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultSemiCompulsoryEvent : MonoBehaviour
{
    [Header("Trigger Events")]
    [SerializeField] PlayerTriggerEventController thisEventTrigger;
    [SerializeField] PlayerTriggerEventController cultTriggerEvent;
    [Header("Player")]
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform setPlayerPoint;
    [Header("Cult")]
    [SerializeField] CultTalkManager cultTalkManager;

    public void SemiCompulsoryCultTalk()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(CompulsoryInteraction, 0, false));
    }

    private void CompulsoryInteraction()
    {
        cultTalkManager.CallDisplayTalk();
        SetPlayerPoint();
        cultTriggerEvent.isEnable = false;
    }

    public void DisableEvent()
    {
        thisEventTrigger.isEnable = false;
    }

    private void SetPlayerPoint()
    {
        playerTransform.position = new Vector3(
            setPlayerPoint.position.x,
            playerTransform.position.y,
            setPlayerPoint.position.z);
        playerTransform.rotation = setPlayerPoint.rotation;
    }
}
