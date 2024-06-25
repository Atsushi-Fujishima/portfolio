using Prototype5;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FistController : MonoBehaviour
{
    [Header("Setting")]
    public bool isLeft = true;
    [Header("Component")]
    [SerializeField] HandCollisionController_5 handCollisionController;
    [SerializeField] HandHapticManager handHapticManager;
    [Header("Action Reference")]
    [SerializeField] InputActionReference triggerButton;
    [SerializeField] InputActionReference gripButton;
    [SerializeField] InputActionReference velocityInfo;
    [Header("Game Object")]
    [SerializeField] private GameObject handGameObject;
    [SerializeField] private GameObject fistGameObjecct;
    [Header("Punch Setting")]
    [SerializeField] private float punchFlagSpeed = 2.0f;
    [Header("Collider")]
    [SerializeField] BoxCollider handCollider;
    [SerializeField] BoxCollider punchCollider;
    [Header("Effects")]
    [SerializeField] AudioSource punchAudioSource;
    [SerializeField] AudioClip punchClip;
    [SerializeField] AudioClip hitPunchClip;

    private bool isFist = false;
    private bool isPunch = false;

    private void Update()
    {
        FistControl();

        if (isFist) PunchControl();
    }

    private void FistControl()
    {
        if (triggerButton.action.IsPressed() && gripButton.action.IsPressed())
        {
            if (isFist == false)
            {
                isFist = true;
                FistAppearanceControl();
            }
        }
        else
        {
            if (isFist)
            {
                isFist = false;
                FistAppearanceControl();
            }
        }
    }

    private void FistAppearanceControl()
    {
        handGameObject.SetActive(!isFist);
        fistGameObjecct.SetActive(isFist);

        handCollider.enabled = !isFist;
    }

    private void PunchControl()
    {
        var velocity = velocityInfo.action.ReadValue<Vector3>();
        var spd = velocity.sqrMagnitude;

        if (spd > punchFlagSpeed * punchFlagSpeed)
        {
            if (isPunch == false)
            {
                // パンチの実行
                isPunch = true;
                // なんらかの処理
                //ColliderControl();
                punchAudioSource.PlayOneShot(punchClip);
            }
        }
        else
        {
            if (isPunch)
            {
                // パンチの終了
                isPunch = false;
            }
        }
    }

    public void CallHitHand()
    {
        if (isLeft)
        {
            handHapticManager.LeftHandHaptic(1.0f, 0.5f);
        }
        else
        {
            handHapticManager.RightHandHaptic(1.0f, 0.5f);
        }

        punchAudioSource.PlayOneShot(hitPunchClip);
    }
}
