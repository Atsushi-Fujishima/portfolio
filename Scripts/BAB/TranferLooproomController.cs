using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranferLooproomController : MonoBehaviour
{
    [SerializeField] GazeController gazeController;
    [SerializeField] LB_GameController lB_GameController;
    [SerializeField] PlayerLoopTeleport upstairsLoopTeleport;
    [SerializeField] PlayerLoopTeleport lastBattleLoopTeleport;
    [SerializeField] PlayerLoopTeleport lastRunTeleport;
    [SerializeField] PlayerLoopTeleport shortcutTeleport;
    [SerializeField] PlayerTriggerEventController stairsTriggerEvent;
    [SerializeField] GameObject[] stairsObstacle = new GameObject[3];
    [SerializeField] GameObject lightGroup;
    [SerializeField] GameObject lastBattleLevel;
    [Header("Last Battle State")]
    public string[] lastBatteleStates = { "Battle", "Run" }; 
    private string lastBattleState = string.Empty;
    [Header("Next Loop")]
    [SerializeField] GameObject lastLoopRoom;

    private bool isFirst = true;

    private void Start()
    {
        lastBattleState = lastBatteleStates[0];
    }

    private void OnEnable()
    {
        OpenStairs();
    }

    private void OnDisable()
    {
        if (this.gameObject != null)
        {
            if (stairsObstacle[0] != null && stairsObstacle[1] != null && stairsObstacle[2] != null)
            {
                CloseStairs();
            }
        }
    }

    private void OpenStairs()
    {
        foreach (GameObject obstacle in stairsObstacle)
        {
            obstacle.SetActive(false);
        }

        stairsTriggerEvent.isEnable = true;
    }

    private void CloseStairs()
    {
        foreach (GameObject obstacle in stairsObstacle)
        {
            obstacle.SetActive(true);
        }
    }

    public void StairsInteraction()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(GoUpStairs, 0, true));
    }

    public void UpstairsDoorInteraction()
    {
        gazeController.isPenetration = true;
        stairsTriggerEvent.isEnable = true;
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(TransferLastBattleLevel, 0, true));
    }

    private void GoUpStairs()
    {
        stairsTriggerEvent.isEnable = false;
        upstairsLoopTeleport.PlayerCharacterTeleport();
    }

    private void TransferLastBattleLevel()
    {
        StartCoroutine(TransferLastBattleLevelCor());
    }

    private IEnumerator TransferLastBattleLevelCor()
    {
        lightGroup.SetActive(false);
        lastBattleLevel.SetActive(true);
        yield return null;
        TeleportBrunch();
        yield break;
    }

    public void BackLoopRoom()
    {
        gazeController.isPenetration = false;
        StartCoroutine(BackLoopCor());
    }

    private IEnumerator BackLoopCor()
    {
        if (lastBattleState == lastBatteleStates[0])
        {
            lB_GameController.LastBattleInitialize();
        }
        else
        {
            lB_GameController.LastRunInitialize();
        }
        
        yield return null;
        lastBattleLevel.SetActive(false);
        lightGroup.SetActive(true);
        yield break;
    }

    public void SetLastBattleState(string _state)
    {
        lastBattleState = _state;
    }

    public void TransferLastRoop()
    {
        lastBattleLevel.SetActive(false);
        lightGroup.SetActive(true);
        CloseStairs();
        lastLoopRoom.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void TeleportBrunch()
    {
        if (lastBattleState == lastBatteleStates[0])
        {
            // battle
            if (isFirst)
            {
                isFirst = false;
                lastBattleLoopTeleport.PlayerCharacterTeleport();
            }
            else
            {
                shortcutTeleport.PlayerCharacterTeleport();
            }
        }
        else
        {
            // last run
            lastRunTeleport.PlayerCharacterTeleport();
        }
    }
}
