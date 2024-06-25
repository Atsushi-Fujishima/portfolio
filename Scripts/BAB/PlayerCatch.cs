using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatch : MonoBehaviour
{
    public AudioSource catchAudio;
    public AudioClip catchClip;
    public SphereCollider thisCollider;
    [Space]
    [SerializeField] SisterMoveController sisterMoveController;
    [SerializeField] PlayerDeathTeleport deathTeleport;
    [SerializeField] TakeGazeController eventSisterGaze;
    [SerializeField] DeathEventLookSister deathEventLookSister;
    [Space]
    [SerializeField] GameObject eventSister;
    [Header("Rotation")]
    public bool isRotate = false;
    [SerializeField] Transform playerTransform;
    [Header("Last Battle Only")]
    public bool isLastBattle = false;
    private bool isLastBattle1 = false;
    private bool isTouchDoor = false;
    [SerializeField] PlayerHideController playerHideController;
    [SerializeField] LB_SisterController lb_SisterController;

    private bool isColliderIn = false;

    private void Awake()
    {
        isColliderIn = false;
        if (playerHideController != null && lb_SisterController != null)
        {
            isLastBattle1 = true;
        }
    }

    private void OnEnable()
    {
        isTouchDoor = false;
        isColliderIn = false;
    }

    private void Update()
    {
        if (isLastBattle)
        {
            if (isColliderIn && lb_SisterController.isMove && isLastBattle1)
            {
                isColliderIn = false;
                lb_SisterController.isMove = false;
                Catch();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var hide = other.gameObject.GetComponent<PlayerHideController>();
            if (hide.isHide)
            {
                return;
                
            }
            else
            {
                Catch();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isLastBattle && isLastBattle1)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (playerHideController.isHide == false)
                {
                    isColliderIn = true;
                }
                else
                {
                    return;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isLastBattle && isLastBattle1)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isColliderIn = false;
            }
        }
    }

    public void Catch()
    {
        if (isTouchDoor) return;

        if (isLastBattle)
        {
            deathEventLookSister.SetPlayerCatch(this);
        }

        if (isRotate)
        {
            sisterMoveController.LookPlayer();
        }

        ColliderControl(false);
        PlayerSound();
        eventSister.SetActive(true);     

        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(CallDeathTeleport, 0, true));
        StartCoroutine(LateGazeOn());
    }

    private void CallDeathTeleport()
    {
        deathTeleport.DeathTeleport();
    }

    private void PlayerSound()
    {
        catchAudio.PlayOneShot(catchClip);
    }

    public void ColliderControl(bool value)
    {
        thisCollider.enabled = value;
    }

    private IEnumerator LateGazeOn()
    {
        yield return new WaitForSeconds(3.0f);
        eventSisterGaze.isEnable = true;

        if (isRotate)
        {
            sisterMoveController.MoveInitialize();
        }

        yield break;
    }

    public void TouchDoor()
    {
        isTouchDoor = true;
    }
}
