using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEventSpecialType : MonoBehaviour
{
    [SerializeField] DeathEventController deathController;
    [SerializeField] TranferLooproomController transferLoopController;
    [Space]
    [SerializeField] GameObject eventSister;
    [SerializeField] Transform target;
    [SerializeField] AudioSource mAudio;
    [SerializeField] AudioClip mAudioClip;
    [Header("Settings")]
    public float eventStartDelay = 3.0f;
    public float waitTime = 1.0f;
    [SerializeField] Vector3 startPosition = Vector3.zero;
    [SerializeField] float startSpeed = 5.0f;
    [SerializeField] float nearDistance = 1.5f;

    private Transform sisterTransform;
    private bool isMoving = true;

    private void Start()
    {
        sisterTransform = eventSister.transform;
    }

    public void CallSpecialSiterDeathEventStart()
    {
        StartCoroutine(DeathEvent());
        MyStaticMethod.DisplayColorLog("r", this.name, "CallSpecialSiterDeathEventStart", "");
    }

    private void EventInitializedBlinkEye()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(EventInitialized, 0, true));
    }

    private void EventInitialized()
    {
        sisterTransform.position = startPosition;
        eventSister.SetActive(false);
        deathController.EndDeathEvent();
        transferLoopController.BackLoopRoom();
    }

    private IEnumerator DeathEvent()
    {
        eventSister.SetActive(true);
        isMoving = true;
        MyStaticMethod.DisplayColorLog("r", this.name, "eventSister.SetActive(true)", "");

        yield return new WaitForSeconds(eventStartDelay);
        MyStaticMethod.DisplayColorLog("r", this.name, "endStartDelay", "");

        while (isMoving)
        {
            if (GetDistance() > nearDistance * nearDistance)
            {
                float distanceToMove = MathF.Min(GetDistance(), startSpeed * Time.deltaTime);
                sisterTransform.position = Vector3.MoveTowards(sisterTransform.position, target.position, distanceToMove);
                yield return null;
            }
            else
            {
                isMoving = false;
            }
        }

        mAudio.PlayOneShot(mAudioClip);
        yield return new WaitForSeconds(waitTime);
        EventInitializedBlinkEye();
        yield break;
    }

    private float GetDistance()
    {
        var offset = sisterTransform.position - target.position;
        var sqrLength = offset.sqrMagnitude;
        return sqrLength;
    }
}
