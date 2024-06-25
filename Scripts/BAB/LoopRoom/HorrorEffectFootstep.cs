using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorEffectFootstep : MonoBehaviour
{
    public bool isStart = false;
    public bool isPause = false;

    [SerializeField] private Animator mAnimator;
    [Header("Setting")]
    [SerializeField] private float startDelay = 5.0f;
    [SerializeField] private string paramName = string.Empty;
    [SerializeField] private float endTime = 20.0f;
    private float elapsedTime = 0f;

    private void Update()
    {
        if (isStart)
        {
            TimeControl();
        }
    }

    public void StartHorrorEffect()
    {
        StartCoroutine(ActivationEffect());
    }

    private IEnumerator ActivationEffect()
    {
        yield return new WaitForSeconds(startDelay);
        
        isStart = true;
        Active();
        yield break;
    }

    private void Active()
    {
        mAnimator.SetBool(paramName, true);
    }

    private void TimeControl()
    {
        if (isPause) return; // Stop this function while paused

        elapsedTime += Time.deltaTime;
        if (elapsedTime > endTime)
        {
            EndEffect();
            elapsedTime = 0f;
        }
    }

    private void EndEffect()
    {
        isStart = false;
        mAnimator.SetBool(paramName, false);
    }
}
