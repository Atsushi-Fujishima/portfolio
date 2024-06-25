using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorEffectStairs : MonoBehaviour
{
    private bool isStart = false;
    [SerializeField] private HorrorEffectFootstep effectFootstep;
    [SerializeField] private BoxCollider triggerCollider;
    [SerializeField] private Animator mAnimator;
    [SerializeField] private SoundEffectNaturalExpression soundEffectNaturalExpression;
    [Header("Setting")]
    [SerializeField] private float endTime = 8.0f;
    [SerializeField] private string paramName = string.Empty;
    [SerializeField] private float changeClipTime = 2.0f;
    [SerializeField] private AudioClip changeClip;
    private float elapsedTime = 0f;
    private bool isChangeClip = false;

    private void Update()
    {
        if (isStart)
        {
            TimeControl();
        }
    }

    public void StartHorrorEffect()
    {
        triggerCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            if (isStart == false) 
                Active();
        }
    }

    private void Active()
    {
        if (effectFootstep.isStart)
        {
            effectFootstep.isPause = true;
        }

        isStart = true;
        mAnimator.SetBool(paramName, true);
    }

    private void TimeControl()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > changeClipTime)
        {
            if (isChangeClip == false)
            {
                soundEffectNaturalExpression.clips[0] = changeClip;
                isChangeClip = true;
            }
        }

        if (elapsedTime > endTime)
        {
            elapsedTime = 0f;
            EndEffect();
        }
    }

    private void EndEffect()
    {
        isStart = false;
        mAnimator.SetBool(paramName, false);
        effectFootstep.isPause = false;
    }
}
