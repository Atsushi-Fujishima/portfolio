using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoopRoomController : MonoBehaviour
{
    [SerializeField] LoopRoomDoorController doorController;
    private BlinkEyeSystem eyeSystem;
    public float openDoorDelayTime = 1.0f;

    private void Start()
    {
        eyeSystem = BlinkEyeSystem.instance;
    }

    public void FirstOpenDoorEvent()
    {
        StartCoroutine(eyeSystem.BlinkEye(FirstOpenDoor, openDoorDelayTime, true));
    }

    private void FirstOpenDoor()
    {
        doorController.OpenStartDoor();
    }
}
