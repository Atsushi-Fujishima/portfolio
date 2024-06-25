using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB_GameController : MonoBehaviour
{
    [Header("Game System")]
    [SerializeField] TranferLooproomController transferLoopController;
    [Header("Player")]
    [SerializeField] PlayerLoopTeleport loopTeleport;
    [SerializeField] PlayerLoopTeleport lastRunTeleport;
    [SerializeField] PlayerLoopTeleport reLoopTeleport;
    [SerializeField] PlayerLoopTeleport magicRoomTeleport;
    [Header("Part One")]
    [SerializeField] LB_PartOneManager partOneManager;
    [SerializeField] GameObject partOne;
    [SerializeField] PlayerTriggerEventController[] triggerEvents;
    [SerializeField] MeshRenderer[] interactionDoorLightRenderer;
    [SerializeField] Light[] doorLights;
    [SerializeField] Color32 doorLightsLoopColor;
    [SerializeField] Color32 doorLightsInitColor;
    [SerializeField] Material changeLightMaterial;
    [SerializeField] Material lightInitializeMaterial;
    [SerializeField] PlayerTriggerEventController partOneStartTrigger;
    [SerializeField] LB_GimmickManager gimmickManager;
    [Space]
    [Header("Part Two")]
    [SerializeField] GameObject partTwo;
    [SerializeField] GameObject startRoomWall;
    [SerializeField] LB_LastSisterMoveController lastSisterMoveController;

    private void Start()
    {
        DoorTriggerEvent(false);
    }

    public void TransferLastRunDoorInteraction()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(TransferPartTwo, 0, true));
    }

    public void FailureDoorInteraction()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(PartOneLoop, 0, true));
    }

    public void SuccessDoorNextLoopInteraction()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(PartOneNextLoop, 0, true));
    }

    private void PartOneLoop()
    {
        PartOneInitialize();
        // loopTeleport.PlayerCharacterTeleport();
        reLoopTeleport.PlayerCharacterTeleport();
    }

    private void TransferPartTwo()
    {
        transferLoopController.SetLastBattleState(transferLoopController.lastBatteleStates[1]); //Specifying the initialization system
        partOne.SetActive(false);
        lastRunTeleport.setRotation = new Vector3(0, 270f, 0);
        lastRunTeleport.PlayerCharacterTeleport();
        partTwo.SetActive(true);
        DoorTriggerEvent(false);
        startRoomWall.SetActive(false);
        gimmickManager.GreenSpel();
    }

    public void GameStartPartOne()
    {
        foreach (var mr in interactionDoorLightRenderer)
        {
            mr.material = changeLightMaterial; // lightのマテリアルを変更
        }

        foreach (var _light in doorLights)
        {
            _light.color = doorLightsLoopColor; // chage lights color
        }

        gimmickManager.ChoiceTruthDoor();
        DoorTriggerEvent(true); // サイドのドアにインタラクトできるようにする
        partOneStartTrigger.isEnable = false; // start trigger disable
    }

    private void PartOneNextLoop()
    {
        foreach (var mr in interactionDoorLightRenderer)
        {
            mr.material = lightInitializeMaterial; // lightのマテリアルを初期化
        }

        foreach (var _light in doorLights)
        {
            _light.color = doorLightsInitColor; // inita lights color
        }

        loopTeleport.setRotation = new Vector3(0, 90f, 0);
        loopTeleport.PlayerCharacterTeleport();
        DoorTriggerEvent(false); // サイドのドアにインタラクトできないようにする
        partOneStartTrigger.isEnable = true;
        partOneManager.InitializationPartOneSystem();
        gimmickManager.UpdateLoop();
    }

    private void PartOneInitialize()
    {
        foreach (var mr in interactionDoorLightRenderer)
        {
            mr.material = lightInitializeMaterial; // lightのマテリアルを初期化
        }

        foreach (var _light in doorLights)
        {
            _light.color = doorLightsInitColor; // inita lights color
        }

        loopTeleport.setRotation = new Vector3(0, 90f, 0);
        DoorTriggerEvent(false); // サイドのドアにインタラクトできないようにする
        partOneStartTrigger.isEnable = true;
        partOneManager.InitializationPartOneSystem();
        gimmickManager.ProgressInitialization();
    }

    private void PartTwoInitialize()
    {
        lastSisterMoveController.LastSisterInitialize();
        partTwo.SetActive(false);
    }

    public void LastBattleInitialize()
    {
        if (partTwo.activeSelf)
        {
            PartTwoInitialize();
            startRoomWall.SetActive(true);
        }

        if (partOne.activeSelf == false)
        {
            partOne.SetActive(true);
        }

        PartOneInitialize();
        gameObject.SetActive(false);
    }

    public void LastRunInitialize()
    {
        lastSisterMoveController.LastSisterInitialize();
        gameObject.SetActive(false);
    }

    private void DoorTriggerEvent(bool _active)
    {
        if (_active)
        {
            foreach (var triggerEvent in triggerEvents)
            {
                triggerEvent.isEnable = true;
                triggerEvent.TagControl();
            }
        }
        else
        {
            foreach (var triggerEvent in triggerEvents)
            {
                triggerEvent.isEnable = false;
                triggerEvent.TagControl();
            }
        }
    }

    public void MagicRoomInteraction()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(MagicRoomTeleport, 0, true));
    }

    private void MagicRoomTeleport()
    {
        magicRoomTeleport.PlayerCharacterTeleport();
    }
}
