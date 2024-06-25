using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerDeathTeleport : MonoBehaviour
{
    [SerializeField] Transform playerCameraOffsetTransform;
    [SerializeField] Transform[] handTransforms;
    [SerializeField] Transform m_TeleportTarget;
    public Vector3 setRotation = Vector3.one;
    [Space]
    [SerializeField] TrackedPoseDriver trackedPoseDriver;
    [SerializeField] PlayerCameraHeightSetting playerCameraHeightSetting;
    [SerializeField] PlayerRotationStick playerRotationStick;
    [Space]
    [SerializeField] GameObject[] physicsHands;
    [SerializeField] GameObject[] virtualHands;

    private Transform playerCameraTransform;

    private void Start()
    {
        playerCameraTransform = Camera.main.gameObject.transform;
    }

    public void DeathTeleport()
    {
        PlayerCharacterTeleport();
        TrackPoseBothControl(false);
        VirtualHandControl(true);
        playerRotationStick.isPermitRotate = false;
    }

    private void PlayerCharacterTeleport()
    {
        PlayerCameraPositionCorrection();
        playerCameraOffsetTransform.rotation = m_TeleportTarget.rotation;

        PlayerCameraPositionCorrection();

        foreach (Transform hand in handTransforms)
        {
            hand.position = m_TeleportTarget.position;
        }
    }

    public void TrackPoseBothControl(bool isBoth)
    {
        if (isBoth)
        {
            trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
        }
        else
        {
            trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
        }
    }

    public void VirtualHandControl(bool isVirtual)
    {
        foreach (GameObject pH in physicsHands)
        {
            pH.SetActive(!isVirtual);
        }

        foreach (GameObject vH in virtualHands)
        {
            vH.SetActive(isVirtual);
        }
    }

    public void DeathInitialized()
    {
        playerCameraOffsetTransform.localPosition= new Vector3(
            0,
            playerCameraHeightSetting.GetSetCameraHeight(),
            0);

        playerCameraOffsetTransform.localRotation = Quaternion.identity;

        TrackPoseBothControl(true);
        VirtualHandControl(false);
        playerRotationStick.isPermitRotate = true;
    }

    private void PlayerCameraPositionCorrection()
    {
        // Move camera using offset
        playerCameraOffsetTransform.position = m_TeleportTarget.position;
        // Calculate the position difference between target and camera
        Vector3 targetToCameraDifferencePosition = m_TeleportTarget.position - playerCameraTransform.position;
        // Correct the Offset position using the difference
        playerCameraOffsetTransform.position += targetToCameraDifferencePosition;
    }
}
