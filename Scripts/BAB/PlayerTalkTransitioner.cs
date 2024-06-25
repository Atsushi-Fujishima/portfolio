using Prototype5;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerTalkTransitioner : MonoBehaviour
{
    [Header("Fade Shader")]
    [SerializeField] MeshRenderer cameraFadeRenderer;
    public float fadeTransValue = 1.0f;
    private Material mat_fade;
    private readonly string alphaCodeName = "_AlphaValue";
    private readonly float alphaTransComplement = 0.02f;
    private bool fadeIn = false;
    private bool fadeOut = false;
    [Header("Player Control")]
    [SerializeField] HandCollisionController_5[] handCollisionControllers;
    [SerializeField] PlayerRotationStick playerRotation;
    [Header("Camera Control")]
    [SerializeField] TrackedPoseDriver trackedPoseDriver;
    [SerializeField] Transform cameraOffset;
    private Transform playerTransform;
    private Rigidbody playerBody;

    private void Awake()
    {
        playerTransform = transform;
        playerBody = GetComponent<Rigidbody>();
        cameraFadeRenderer.gameObject.SetActive(true);
    }

    private void Start()
    {
        mat_fade = cameraFadeRenderer.material;
        if (cameraFadeRenderer.gameObject.activeSelf) cameraFadeRenderer.gameObject.SetActive(false);
    }

    public void ChangeCameraControl(Vector3 _setPosition)
    {
        //trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
        cameraOffset.position = _setPosition;
        playerBody.isKinematic = true;
    }

    public void StopPlayerControl()
    {
        //don't move
        foreach (var controller in handCollisionControllers)
        {
            controller.isNotDetect = true;
        }

        //don't rotate
        playerRotation.isPermitRotate = false;
    }

    public void OnPlayerCamRotate(Vector3 _offset)
    {
        playerTransform.rotation = Quaternion.identity;
        cameraOffset.localEulerAngles = _offset;
    }

    public void OnPlayerCamRotationInit()
    {
        cameraOffset.rotation = Quaternion.identity;
    }

    public void OnPlayerCameraFadeOut(float _fadeValue)
    {
        fadeTransValue = _fadeValue;
        if (cameraFadeRenderer.gameObject.activeSelf == false) cameraFadeRenderer.gameObject.SetActive(true);
        fadeOut = true;
        StartCoroutine(CamFadeOut());
    }

    public void OnPlayerCameraFadeIn(float _fadeValue)
    {
        fadeTransValue = _fadeValue;
        fadeIn = true;
        StartCoroutine (CamFadeIn());
    }

    private IEnumerator CamFadeIn()
    {
        while (fadeIn == true)
        {
            yield return null;
            var a = GetAlphaValue();
            if (a < 0.1)
            {
                fadeIn = false; 
                SetAlphaValue(0f);
                cameraFadeRenderer.gameObject.SetActive(false);
                yield break;
            }

            a -= fadeTransValue * alphaTransComplement;
            SetAlphaValue(a);
        }
    }

    private IEnumerator CamFadeOut()
    {
        while (fadeOut == true)
        {
            yield return null;
            var a = GetAlphaValue();
            if (a > 0.95f)
            {
                fadeOut = false;
                SetAlphaValue(1f);
                yield break;
            }

            a += fadeTransValue * alphaTransComplement;
            SetAlphaValue(a);
        }
    }

    private float GetAlphaValue()
    {
        return mat_fade.GetFloat(alphaCodeName);
    }

    private void SetAlphaValue(float _value)
    {
        mat_fade.SetFloat(alphaCodeName, _value);
    }
}
