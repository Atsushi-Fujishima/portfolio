using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class WinBattleController : MonoBehaviour
{
    [Header("Game Systems")]
    [SerializeField] PlayerLoopTeleport playerLoopTeleport;
    [SerializeField] TranferLooproomController transferLooproomController;
    [Header("Teleport Point")]
    [SerializeField] Transform teleportPoint;
    [Header("Visual Effests")]
    [SerializeField] ShaderEffectManager spelEffectManager;
    [SerializeField] ShaderEffectManager bookEffectManager;
    [SerializeField] ParticleSystem fireEffectParent;
    [SerializeField] ParticleSystem[] fireEffects;
    [SerializeField] ParticleSystem spelEffect;
    [Header("Sound Effects")]
    [SerializeField] AudioSource fireAudio;
    [SerializeField] AudioSource ignitionAudio;
    [SerializeField] AudioSource sisterAudio;
    [SerializeField] AudioClip ignitionSound;
    [SerializeField] AudioClip screamSound;
    [SerializeField] SoundVolumeChanger fireVolumeChanger;
    [Header("Animations")]
    [SerializeField] Animator bookAnimator;
    [Header("Lights")]
    [SerializeField] Light redLight;
    [Header("Settings")]
    [SerializeField] float extinctionDelay = 1.0f;
    [SerializeField] float endEventdelay = 3.0f;
    [SerializeField] float shaderEffectExtinctionTime = 2.0f;
    [SerializeField] float shaderEffectExtinctionValue = 0.85f;
    private float extinctionElapsedTime = 0f;

    public void InteractionBook()
    {
        StartCoroutine(WinBattleEvent());
    }

    private IEnumerator WinBattleEvent()
    {
        bookAnimator.SetBool("Stop", true);
        spelEffect.Stop();
        redLight.enabled = false;
        StartCoroutine(BookBurns());
        yield return new WaitForSeconds(extinctionDelay);
        StartCoroutine(ExtinctionShaderEffect());
        sisterAudio.PlayOneShot(screamSound);
        yield return new WaitForSeconds(endEventdelay);
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(TransferLastLoop, 0, true)); // blink & teleport & end LB
        
        yield break;
    }

    private void TransferLastLoop()
    {
        transferLooproomController.TransferLastRoop();
        // LoopRoom Control

        // Teleport player
        playerLoopTeleport.setRotation = Vector3.zero;
        playerLoopTeleport.SetLoopPoint(teleportPoint);
        playerLoopTeleport.PlayerCharacterTeleport();
    }

    private IEnumerator BookBurns()
    {
        fireEffectParent.Play();
        ignitionAudio.PlayOneShot(ignitionSound);
        yield return new WaitForSeconds(0.3f);    
        fireAudio.Play();
        yield return new WaitForSeconds(shaderEffectExtinctionTime - 0.5f);
        
        foreach (ParticleSystem ps in fireEffects)
        {
            var mainSystem = ps.main;
            mainSystem.loop = false;
        }

        fireVolumeChanger.FadeOutVolume();
        yield break;
    }

    private IEnumerator ExtinctionShaderEffect()
    {
        while (extinctionElapsedTime < shaderEffectExtinctionTime)
        {
            extinctionElapsedTime += Time.deltaTime;
            float progress = extinctionElapsedTime / shaderEffectExtinctionTime;
            var value = Mathf.Lerp(0f, shaderEffectExtinctionValue, progress);
            spelEffectManager.SetFloatValue(0, value);
            bookEffectManager.SetFloatValue(0, value);
            yield return null;
        }

        spelEffectManager.SetFloatValue(0, 1f);
        bookEffectManager.SetFloatValue(0, 1f);
        yield break;
    }
}
