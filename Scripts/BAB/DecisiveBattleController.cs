using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisiveBattleController : MonoBehaviour
{
    [Header("Destruction Effect")]
    [SerializeField] HandHapticManager handHapticManager;
    [SerializeField] float hapticTime = 1.0f;
    [SerializeField] float hapticPower = 1.0f;
    [SerializeField] GameObject fire;
    [SerializeField] AudioSource screamAudio;
    [Header("Obstacle")]
    [SerializeField] GameObject[] obstacles;
    [Header("Sister")]
    [SerializeField] GameObject sister;
    [Header("Teloport")]
    [SerializeField] PlayerLoopTeleport loopTeleport;
    [Header("Room Control")]
    [SerializeField] GameObject lastRoom;
    [SerializeField] ExitDoorController exitDoorController;
    [Header("Hiden Door")]
    [SerializeField] GameObject door;
    public float setRotation = 0;
    [Header("Miss Interaction")]
    [SerializeField] AudioSource missAudio;

    private void OnEnable()
    {
        ObstacleControl(false);
    }

    private void OnDisable()
    {
        ObstacleControl(true); 
    }

    public void DestructionOfRitual()
    {
        StartCoroutine(Destruction());
    }

    private IEnumerator Destruction()
    {
        sister.SetActive(false); // siste off
        screamAudio.Play(); // sakebigoe
        fire.SetActive(true); // gisiki hakai effect
        handHapticManager.BothHandHaptic(hapticPower, hapticTime); // sinndou

        yield return new WaitForSeconds(3.0f);

        // blink call end loop
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(EndLoop, 1.5f, true));


        yield break;
    }

    private void EndLoop()
    {
        loopTeleport.PlayerCharacterTeleport(); // teleport
        lastRoom.SetActive(true); // L9 setActive true
        exitDoorController.isEndLoop = true;
        StartCoroutine(DelayDisable());
    }

    private IEnumerator DelayDisable()
    {
        yield return new WaitForSeconds(3.0f);
        this.gameObject.SetActive(false);
    }

    private void ObstacleControl(bool _active)
    {
        if (obstacles == null) return;

        if (_active)
        {
            foreach (var obj in obstacles)
            {
                if (obj == null) //Fixed an error that would occur when exiting the game in the editor.
                {
                    return;
                }

                obj.SetActive(true);
            }
        }
        else
        {
            foreach (var obj in obstacles)
            {
                if (obj == null) //Fixed an error that would occur when exiting the game in the editor.
                {
                    return;
                }

                obj.SetActive(false);
            }
        }
    }

    public void OpenHidenDoor()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(Open, 0, true));
    }

    private void Open()
    {
        transform.rotation = Quaternion.Euler(0, 85f, 0);
    }

    public void MissInteraction()
    {
        missAudio.Play();
    }
}
