using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TakeGazeController : MonoBehaviour
{
    [Header("State")]
    public bool isEnable = false;
    [Header("Setting")]
    public bool isOneced = true;
    public bool isTargetMovement = false;
    public Transform thisTarget;

    [Header("Events")]
    public UnityEvent OnCallEvent;

    [Header("Haptic")]
    [SerializeField] HandHapticManager handHaptic;
    [SerializeField] float hapticAmplitude = 0.5f;
    [SerializeField] float hapticDuration = 0.5f;

    private bool isPermit = true;

    private void LateUpdate()
    {
        if (isTargetMovement)
        {
            TrackingTarget();
        }
    }

    private void TrackingTarget()
    {
        transform.position = thisTarget.position;
    }

    public void TakeGaze()
    {
        if (isEnable == false)
            return;

        if (isOneced)
        {
            isOneced = false;
            isEnable = false;
            handHaptic.BothHandHaptic(hapticAmplitude, hapticDuration);
            OnCallEvent?.Invoke();
        }
        else
        {
            if (isPermit == false) return;

            isPermit = false;
            handHaptic.BothHandHaptic(hapticAmplitude, hapticDuration);
            OnCallEvent?.Invoke();
            StartCoroutine(TimeWaitPermit());
        }
    }

    public void EnableGazeTrigger()
    {
        isEnable = true;
    }

    private IEnumerator TimeWaitPermit()
    {
        yield return new WaitForSeconds(3);

        isPermit = true;

        yield break;
    }
}
