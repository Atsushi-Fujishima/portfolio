using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameGuideController : MonoBehaviour
{
    [Header("Player Control")]
    [SerializeField] Prototype5.PlayerCharacterManager_5 playerCharacterManager;
    [SerializeField] Transform playerCharacterTransform;
    [SerializeField] Transform playerLookPoint;
    [Header("Event Items")]
    [SerializeField] PlayerTriggerEventController[] triggerEventControllers;
    [SerializeField] TakeGazeController takeGazeController;
    [SerializeField] TriggerActivatationController[] triggerActivatationControllers;
    [Header("Game Guide Settings")]
    [SerializeField] float babyMoveStartDelay = 2.0f;
    [SerializeField] float babyMoveInterval = 2.0f;
    [Header("Babys")]
    [SerializeField] GameGuideBabyMover[] babyMovers;

    private int guideEndFlags = 3;
    private int guideProgress = 0;

    public void StartGameSystemGuide()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(StartGuide, 0, true));
    }

    private void EndGameSystemGuide()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(EndGuide, 0, true));
    }

    private void StartGuide()
    {
        IsPlayerControl(false);
        SetPlayerPoint();
        EnableTriggerEvents();
        StartCoroutine(StartBabysMove());
    }

    private void EndGuide()
    {
        IsPlayerControl(true);
    }

    private IEnumerator StartBabysMove()
    {
        yield return new WaitForSeconds(babyMoveStartDelay);
        babyMovers[0].isMove = true;
        yield return new WaitForSeconds(babyMoveInterval);
        babyMovers[1].isMove = true;
        yield return new WaitForSeconds(babyMoveInterval);
        babyMovers[2].isMove = true;
        yield break;
    }

    private void IsPlayerControl(bool isControl)
    {
        if (isControl)
        {
            playerCharacterManager.isNotMove = false;
            playerCharacterManager.isNotRotate = false;
        }
        else
        {
            playerCharacterManager.isNotMove = true;
            playerCharacterManager.isNotRotate = true;
        }
    }

    private void SetPlayerPoint()
    {
        playerCharacterTransform.position = playerLookPoint.position;
        playerCharacterTransform.rotation = playerLookPoint.rotation;
    }

    private void EnableTriggerEvents()
    {
        foreach (var triggerEventController in triggerEventControllers)
        {
            triggerEventController.isEnable = true;
        }

        takeGazeController.isEnable = true;
    }

    public void BabyReachTargetPoint(int _targetCode)
    {
        EventItemActivation(_targetCode);
        guideProgress++;
        if (guideProgress == guideEndFlags)
        {
            EndGameSystemGuide();
        }
    }

    public void EventItemActivation(int _targetCode)
    {
        triggerActivatationControllers[_targetCode].Activate();
    }
}
