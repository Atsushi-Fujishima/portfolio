using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEventLookSister : MonoBehaviour
{
    [SerializeField] GameObject eventSister;
    [SerializeField] PlayerDeathTeleport deathTeleport;
    [SerializeField] PlayerCatch playerCatch;
    [SerializeField] PlayerLoopTeleport loopTeleport;
    [SerializeField] TakeGazeController eventSisterGaze;
    [Space]
    [SerializeField] AudioSource sisterAudio;
    [SerializeField] AudioClip clip;

    public void InteractionLookSister()
    {
        eventSisterGaze.isEnable = false;
        sisterAudio.PlayOneShot(clip);
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(EndDeathEvent, 0, true));
    }

    private void EndDeathEvent()
    {
        eventSister.SetActive(false);
        deathTeleport.DeathInitialized();
        playerCatch.ColliderControl(true);
        loopTeleport.PlayerCharacterTeleport();
    }

    public void SetPlayerCatch(PlayerCatch value)
    {
        playerCatch = value;
    }
}
