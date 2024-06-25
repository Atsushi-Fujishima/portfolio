using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPunchEventController : MonoBehaviour
{
    [Header("State")]
    public bool isEnable = true;
    [Header("Setting")]
    public bool isOneced = true;
    public float delayTime = 3.0f;

    private readonly int targetLayer = 14;

    [Header("Events")]
    public UnityEvent OnCallEvent;

    private bool isBreak = false;
    private bool isPermit = true;

    private void OnCollisionEnter(Collision other)
    {
        if (isEnable == false)
            return;

        if (isBreak || isPermit == false)
            return;

        if (other.gameObject.layer == targetLayer)
        {
            OnCallEvent.Invoke();
            if (isOneced == true) isBreak = true;

            var controller = other.gameObject.GetComponent<FistController>();
            controller.CallHitHand();
        }

        isPermit = false;
        StartCoroutine(TimeWaitPermit());
    }

    private void OnCollisionStay(Collision other)
    {
        if (isEnable == false)
            return;

        if (isBreak || isPermit == false)
            return;

        if (other.gameObject.layer == targetLayer)
        {
            OnCallEvent.Invoke();
            if (isOneced == true) isBreak = true;

            var targetParent = other.gameObject.transform.parent;
            var controller = targetParent.gameObject.GetComponent<FistController>();
            controller.CallHitHand();
        }

        isPermit = false;
        StartCoroutine(TimeWaitPermit());
    }

    private IEnumerator TimeWaitPermit()
    {
        yield return new WaitForSeconds(delayTime);

        isPermit = true;

        yield break;
    }

    public bool IsBreak()
    {
        return isBreak;
    }
}
