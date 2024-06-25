using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB_GimmickController : MonoBehaviour
{
    public enum GimmickType { Fake, Truth };
    [SerializeField] LB_GameController gameController;
    [SerializeField] LB_GimmickManager gimmickManager;
    [Space]
    public GimmickType gimmickType = GimmickType.Fake;
    [Header("Loop 1")]
    [SerializeField] GameObject fakeGimmick_1;
    [SerializeField] GameObject truthGimmick_1;
    [Header("Loop 2")]
    [SerializeField] GameObject fakeGimmick_2;
    [SerializeField] GameObject truthGimmick_2;
    [Header("Loop 3")]
    [SerializeField] GameObject fakeGimmick_3;
    [SerializeField] GameObject truthGimmick_3;

    public void SetFakeGimmick(int _LoopNumber)
    {
        AllHideGimmick();

        if (_LoopNumber == 0) 
        {
            gimmickType = GimmickType.Fake;
            fakeGimmick_1.SetActive(true);
            truthGimmick_1.SetActive(false);
        }
        else if (_LoopNumber == 1)
        {
            gimmickType = GimmickType.Fake;
            fakeGimmick_2.SetActive(true);
            truthGimmick_2.SetActive(false);
        }
        else
        {
            gimmickType = GimmickType.Fake;
            fakeGimmick_3.SetActive(true);
            truthGimmick_3.SetActive(false);
        }
        
    }

    public void SetTruthGimmick(int _LoopNumber)
    {
        AllHideGimmick();

        if (_LoopNumber == 0)
        {
            gimmickType = GimmickType.Truth;
            fakeGimmick_1.SetActive(false);
            truthGimmick_1.SetActive(true);
        }
        else if (_LoopNumber == 1)
        {
            gimmickType = GimmickType.Truth;
            fakeGimmick_2.SetActive(false);
            truthGimmick_2.SetActive(true);
        }
        else
        {
            gimmickType = GimmickType.Truth;
            fakeGimmick_3.SetActive(false);
            truthGimmick_3.SetActive(true);
        }
    }

    public void InteractionGimmickDoor()
    {
        if (gimmickType == GimmickType.Truth)
        {
            gimmickManager.LoopControl();
        }
        else
        {
            gameController.FailureDoorInteraction();
        }
    }

    public void AllHideGimmick()
    {
        fakeGimmick_1.SetActive(false);
        truthGimmick_1.SetActive(false);
        fakeGimmick_2.SetActive(false);
        truthGimmick_2.SetActive(false);
        fakeGimmick_3.SetActive(false);
        truthGimmick_3.SetActive(false);
    }
}
