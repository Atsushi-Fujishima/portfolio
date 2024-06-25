using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LoopController : MonoBehaviour
{
    [Header("LoopRoom")]
    private LoopController thisLoop;
    public LoopController nextLoop;
    public bool[] roomTrigger;
    private bool isCompletionAllTrigger = false;
    [Header("ExitDoor")]
    public ExitDoorController exitDoorController;
    [Header("Trigger Events")]
    public PlayerTriggerEventController[] playerTriggerEventControllers;
    public string triggerObjectTag = "InteractObject";
    public string initObjectTag = "Untagged";
    [Header("Sign")]
    [SerializeField] bool isPermitSign = true; 
    [SerializeField] bool isPlaySign = true;
    [SerializeField] AudioSource mAudio;
    [SerializeField] AudioClip mAudioClip;
    [SerializeField, Range(0, 1.0f)] float signVolume = 1.0f;
    [Header("Progressable Spel Effect")]
    [SerializeField] MeshRenderer spelRenderer;
    [SerializeField] float defaultAlphaValue = 0.2f;
    [SerializeField] float effectAlphaValue = 1.0f;
    private Material spelMaterial;
    [Header("Progressable Light Effect")]
    public bool isInitializeStart = true;
    [SerializeField] MeshRenderer lightRenderer;
    [SerializeField] Material loopDoorDefaultMaterial;
    [SerializeField] Material loopDoorEffectMaterial;
    [SerializeField] Light loopDoorLight;
    [SerializeField] Color32 defaultLightColor = Color.white;
    [SerializeField] Color32 effectLightColor = Color.green;
    [Header("Bathroom")]
    [SerializeField] BathroomController bathroomController;
    public bool isClose = false;
    public bool isHalfOpen = false;
    public bool isOpen = false;

    private void Start()
    {
        thisLoop = this;
        BathroomDoorControl();
        ActivationPlayerTriggerFlags();
        mAudio.volume = signVolume;
        spelMaterial = spelRenderer.material;

        DisableProgressableEffect();
    }

    private void Update()
    {
        if (isCompletionAllTrigger)
        {
            SignControl();
        }
    }

    private void SignControl()
    {
        if (isPermitSign == false) return;

        if (isPlaySign == false)
        {
            isPlaySign = true;
            PlaySign();
        }
    }

    public void PlaySign()
    {
        mAudio.PlayOneShot(mAudioClip);
    }

    public void AchievementTriggger()
    {
        for (int i = 0; i < roomTrigger.Length; i++)
        {
            if (roomTrigger[i] == false)
            {
                roomTrigger[i] = true;
                break;
            }
        }

        LoopTriggerMagement();
    }

    private void LoopTriggerMagement()
    {
        var num = 0;
        foreach (var trigger in roomTrigger)
        {
            if (trigger) num++;
        }

        if (num == roomTrigger.Length)
        {
            isCompletionAllTrigger = true;
            EnableProgressableEffect();
        }
    }

    // call exitDoorController
    public void NextLoopControl()
    {
        if (IsNext())
        {
            LoopChenger();
        }
        else
        {
            return;
        }
    }

    private bool IsNext()
    {
        if (isCompletionAllTrigger)
            return true;
        else
            return false;
    }

    private void LoopChenger()
    {
        if (nextLoop != null)
        {
            // set next Loop
            nextLoop.gameObject.SetActive(true);

            // update loopController in exitDoorController
            exitDoorController.currentLoopController = nextLoop;
        }

        // end this loop
        thisLoop.gameObject.SetActive(false);
    }

    private void ActivationPlayerTriggerFlags()
    {
        if (playerTriggerEventControllers.Length != 0)
        {
            foreach (var ptec in playerTriggerEventControllers)
            {
                ptec.isEnable = true;
                var triggerObject = ptec.gameObject;
                triggerObject.tag = triggerObjectTag;
            }
        }
    }

    private void BathroomDoorControl()
    {
        if (isClose) bathroomController.CloseDoor();
        if (isHalfOpen) bathroomController.HalfOepnDoor();
        if (isOpen) bathroomController.OpenDoor();
    }

    private void EnableProgressableEffect()
    {
        spelMaterial.color = new Color(
            spelMaterial.color.r,
            spelMaterial.color.g,
            spelMaterial.color.b,
            effectAlphaValue);

        lightRenderer.material = loopDoorEffectMaterial;
        loopDoorLight.color = effectLightColor;
        loopDoorLight.intensity = 0.05f;
    }

    private void DisableProgressableEffect()
    {
        spelMaterial.color = new Color(
            spelMaterial.color.r,
            spelMaterial.color.g,
            spelMaterial.color.b,
            defaultAlphaValue);

        if (isInitializeStart)
        {
            lightRenderer.material = loopDoorDefaultMaterial;
            loopDoorLight.color = defaultLightColor;
            loopDoorLight.intensity = 0.02f;
        }
    }
}
