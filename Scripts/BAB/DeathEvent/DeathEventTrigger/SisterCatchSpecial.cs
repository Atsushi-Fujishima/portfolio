using System.Collections;
using UnityEngine;

public class SisterCatchSpecial : MonoBehaviour
{
    public AudioSource catchAudio;
    public AudioClip catchClip;
    public SphereCollider thisCollider;
    public float teleportDelay = 1.0f;
    [Header("Sister Type")]
    public bool isLustRun = false;
    [Header("Sister Controls")]
    [SerializeField] LB_SisterController sisterController;
    [SerializeField] SisterVoiceController sisterVoiceController;
    [Header("Event Controls")]
    [SerializeField] PlayerDeathTeleport deathTeleport;
    [SerializeField] DeathEventController deathEventController;
    [Header("Player")]
    [SerializeField] PlayerHideController playerHideController;
    [SerializeField] PlayerFoundEffectController playerFoundEffectController;
    [SerializeField] PlayerInteractedManager playerInteractedManager;
    
    private bool isTouchDoor = false;
    private bool isTriggerIn = false;

    private void OnEnable()
    {
        isTouchDoor = false;
        isTriggerIn = false;
    }

    private void Update()
    {
        if (isLustRun == false)
        {
            LastBattleSisterTriggerCheck();
        }
    }

    private void LastBattleSisterTriggerCheck()
    {
        if (isTriggerIn && sisterController.isMove)
        {
            isTriggerIn = false;
            sisterController.isMove = false;
            CatchPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerHideController>() == null)
            {
                return;
            }

            if (playerHideController.isHide)
            {
                return;

            }
            else
            {
                CatchPlayer();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerHideController>() == null)
            {
                return;
            }

            if (playerHideController.isHide == false)
            {
                isTriggerIn = true;
            }
            else
            {
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerHideController>() == null)
            {
                return;
            }

            isTriggerIn = false;
        }
    }

    private void CatchPlayer()
    {
        if (isTouchDoor) return;

        if (playerInteractedManager.IsInteracted())
        {
            return;
        }
        else
        {
            playerInteractedManager.DisableInteraction();
        }

        playerFoundEffectController.SetTypeStop();
        thisCollider.enabled = false;
        PlaySound();
        deathEventController.StartDeathEvent(deathEventController.SpecialEventCode(), thisCollider, sisterVoiceController);
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(TeleportAfterBlinkEye, 0, true));
    }

    private void TeleportAfterBlinkEye()
    {
        StartCoroutine(TeleportCor());
    }

    private void PlaySound()
    {
        sisterVoiceController.StopSoliSoundControl();
        catchAudio.PlayOneShot(catchClip);
    }

    private IEnumerator TeleportCor()
    {
        yield return new WaitForSeconds(teleportDelay);
        deathTeleport.DeathTeleport();
    }

    public void PlayerTouchDoor()
    {
        isTouchDoor = true;
    }
}
