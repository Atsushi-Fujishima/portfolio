using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InDoorController : MonoBehaviour
{
    [SerializeField] DoorController inDoor;

    public void InteractionInDoor()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(inDoor.OpenDoor, 0, true));
    }
}
