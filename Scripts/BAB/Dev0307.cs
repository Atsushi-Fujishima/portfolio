using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dev0307 : MonoBehaviour
{
    public LB_GameController gameController;
    public LB_PartOneManager partOneManager;
    public LB_GimmickController gimmickController;

    public void Update()
    {
        var key = Keyboard.current;
        if (key.spaceKey.wasPressedThisFrame)
        {
            gameController.GameStartPartOne();
            partOneManager.ActivateSister();
        }

        if (key.sKey.wasReleasedThisFrame)
        {
            gimmickController.InteractionGimmickDoor();
        }

        if (key.fKey.wasReleasedThisFrame)
        {
            
        }
    }
}
