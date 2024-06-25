using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorController : MonoBehaviour
{
    [SerializeField] PlayerLoopTeleport playerLoopTeleport;
    [SerializeField] PlayerTriggerEventController doorTriggerEventController;
    [SerializeField] DoorController exitDoor;
    [SerializeField] DoorController inDoor;
    [SerializeField] LastLoopRoomController lastLoopRoomController;

    public bool isEndLoop = false;

    [Header("Loop")]
    public LoopController currentLoopController;

    public void InteractionExitDoor()
    {
        if (isEndLoop)
        {
            StartCoroutine(BlinkEyeSystem.instance.BlinkEye(ExitHotel, 0, true));
        }
        else
        {
            StartCoroutine(BlinkEyeSystem.instance.BlinkEye(DoorLoopEvent, 0, true));
        }
    }

    private void DoorLoopEvent()
    {
        inDoor.CloseDoor();
        inDoor.OpenDoor();
        playerLoopTeleport.PlayerCharacterTeleport();

        currentLoopController.NextLoopControl();
    }

    private void ExitHotel()
    {
        exitDoor.OpenDoor();
        doorTriggerEventController.isEnable = false;
        lastLoopRoomController.EndGame();
    }
}
