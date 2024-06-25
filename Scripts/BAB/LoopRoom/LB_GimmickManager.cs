using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB_GimmickManager : MonoBehaviour
{
    [SerializeField] LB_GameController gameController;
    [SerializeField] LB_GimmickController[] gimmickControllers;
    [SerializeField] GameObject[] truthGimmicks;
    [SerializeField] GameObject[] loopEffects;
    public int maxLoop = 3;
    private int loopNum = 0;

    private void Start()
    {
        loopNum = 0;
        TruthGimmickControl();
        HideGimmicks();
    }

    private void HideGimmicks()
    {
        foreach (var controller in gimmickControllers)
        {
            controller.AllHideGimmick();
        }
    }

    public void ChoiceTruthDoor()
    {
        var select = Random.Range(0, gimmickControllers.Length);

        for (int i = 0; i < gimmickControllers.Length; i++)
        {
            if (i == select)
            {
                gimmickControllers[i].SetTruthGimmick(loopNum);
            }
            else
            {
                gimmickControllers[i].SetFakeGimmick(loopNum);
            }
        }
    }

    // called LB_GimmickController
    public void LoopControl()
    {
        loopNum++;
        if (loopNum < maxLoop)
        {
            gameController.SuccessDoorNextLoopInteraction();
        }
        else
        {
            gameController.TransferLastRunDoorInteraction();
        }
    }

    // called LB_GameController
    public void UpdateLoop()
    {
        TruthGimmickControl();
        HideGimmicks();
    }

    private void TruthGimmickControl()
    {
        if (loopNum < maxLoop)
        {
            foreach (var gim in truthGimmicks)
            {
                gim.SetActive(false);
            }

            truthGimmicks[loopNum].SetActive(true);

            foreach (var le in loopEffects)
            {
                le.SetActive(false);
            }

            loopEffects[loopNum].SetActive(true);
        }
        else
        {
            foreach (var gim in truthGimmicks)
            {
                gim.SetActive(false);
            }

            truthGimmicks[0].SetActive(true);

            foreach (var le in loopEffects)
            {
                le.SetActive(false);
            }

            loopEffects[0].SetActive(true);
        }
    }

    public void GreenSpel()
    {
        foreach (var le in loopEffects)
        {
            le.SetActive(false);
        }

        loopEffects[0].SetActive(true);
    }

    // called LB_GameController
    public void ProgressInitialization()
    {
        // loopNum = 0;
        UpdateLoop();
    }
}
