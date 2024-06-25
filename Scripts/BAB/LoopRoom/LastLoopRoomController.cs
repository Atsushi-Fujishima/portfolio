using Prototype5;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class LastLoopRoomController : MonoBehaviour
{
    [SerializeField] GameObject doorSpel;
    [Header("Exit Door")]
    [SerializeField] ExitDoorController exitDoorController;
    [Header("Blink Eye Override")]
    [SerializeField] MeshRenderer fadeRenderer;
    [SerializeField] Color32 overrideFadeColor = Color.white;
    [Header("Sound")]
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource lastBgmAudio;
    [Header("Game End position")]
    [SerializeField] Transform gameEndPoint;
    [Header("Player Camera")]
    [SerializeField] Camera playerCamera;
    [SerializeField] Transform playerCameraOffset;
    [Header("Player Control")]
    [SerializeField] PlayerCharacterManager_5 playerCharacterManager;
    [Header("Dev")]
    [SerializeField] GameObject message;

    private void Start()
    {
        doorSpel.SetActive(false);    
        exitDoorController.isEndLoop = true;
    }

    public void BlinkEyeFadeChangeColor()
    {
        fadeRenderer.material.color = overrideFadeColor;
    }

    public void EndGame()
    {
        // play bgm
        bgm.Stop();
        lastBgmAudio.Play();
        // move player camera
        playerCameraOffset.position = gameEndPoint.position;
        // change camera background Type
        playerCamera.clearFlags = CameraClearFlags.SolidColor;
        playerCamera.backgroundColor = overrideFadeColor;
        
        

        message.SetActive(true);
    }

    public void StopPlayer()
    {
        playerCharacterManager.isNotMove = true;
        // playerCharacterManager.isNotRotate = true;
    }

    public bool GetPlayerNotMove()
    {
        return playerCharacterManager.isNotMove;
    }
}
