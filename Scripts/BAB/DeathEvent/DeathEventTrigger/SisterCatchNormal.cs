using System.Collections;
using UnityEngine;

public class SisterCatchNormal : MonoBehaviour
{
    public AudioSource catchAudio;
    public AudioClip catchClip;
    public SphereCollider thisCollider;
    [Header("Sister Controls")]
    [SerializeField] SisterMoveController sisterMoveController;
    [SerializeField] SisterVoiceController sisterVoiceController;
    [Header("Event Controls")]
    [SerializeField] PlayerDeathTeleport deathTeleport;
    [SerializeField] DeathEventController deathEventController;
    [Header("Reservation an Event")]
    [SerializeField] PlayerInteractedManager interactedManager;
    private bool isReservationEvent = false;

    private PlayerHideController playerHideController;

    private void Update()
    {
        if (isReservationEvent)
            UnderReservationEvent();
    }

    private void UnderReservationEvent()
    {
        if (interactedManager.IsInteracted() == false)
        {
            isReservationEvent = false;
            CatchPlayer();
        }
    }

    private bool IsGetPlayerHideController()
    {
        if (playerHideController == null)
        {
            return false;
        }
        else
        {
            return true;
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

            if (IsGetPlayerHideController() == false)
            {
                playerHideController = other.gameObject.GetComponent<PlayerHideController>();
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

    private void CatchPlayer()
    {
        if (interactedManager.IsInteracted())
        {
            isReservationEvent = true; // book an event
            sisterMoveController.LookPlayer(); // nun is rotate
            return; // Stop the action and book an event
        }
        else
        {
            // Disable player interaction
            interactedManager.DisableInteraction();
        }

        sisterMoveController.LookPlayer(); // nun is rotate
        thisCollider.enabled = false; // stoped trigger enter
        PlaySound(); // play catch sound
        deathEventController.StartDeathEvent(deathEventController.NormalEventCode(), thisCollider, sisterVoiceController); // start event
        StartCoroutine(SisterMoveInitialized()); // start move sister
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(TeleportAfterBlinkEye, 0, true)); // teleport event position
    }

    private void TeleportAfterBlinkEye()
    {
        deathTeleport.DeathTeleport();
    }

    private IEnumerator SisterMoveInitialized()
    {
        yield return new WaitForSeconds(3.0f);
        sisterMoveController.MoveInitialize();
    }

    private void PlaySound()
    {
        sisterVoiceController.StopSoliSoundControl();
        catchAudio.PlayOneShot(catchClip);
    }
}
