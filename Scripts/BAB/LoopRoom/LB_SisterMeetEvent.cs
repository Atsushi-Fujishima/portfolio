using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB_SisterMeetEvent : MonoBehaviour
{
    [SerializeField] HandHapticManager handHapticManager;
    [SerializeField] Animator targetLightAnimator;
    [SerializeField] PlayerFoundEffectController playerFoundEffectController;
    public float delayTime = 5.0f;
    
    private void OnEnable()
    {
        StartCoroutine(DangerDetection());
    }

    private IEnumerator DangerDetection()
    {
        targetLightAnimator.Play("Blinking");
        yield return new WaitForSeconds(delayTime);
        playerFoundEffectController.SetTypeCompulsion();
        handHapticManager.BothHandHaptic(1.0f, 3.0f);
        yield break;
    }
}
