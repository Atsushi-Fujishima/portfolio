using Prototype5;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGiveBoneEvent : MonoBehaviour
{
    public LoopRoomConditionControl loopRoomConditionControl;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform dogTransform;
    [SerializeField] BoxCollider triggerCollider;
    [SerializeField] GameObject interactionObject;
    [SerializeField] Transform prePlayerPoint;
    [SerializeField] Transform preDogPoint;
    [SerializeField] Transform keyItemPoint;
    [SerializeField] GameObject keyItem;
    [SerializeField] GameObject playerGetHandBone;
    [SerializeField] AudioSource dogAudioSource;
    [SerializeField] AudioClip seDogBarck;

    [Header("Setting")]
    [SerializeField] float preparationDelay = 2.0f;

    private bool isPermitGave = false;


    private void OnEnable()
    {
        // 条件が満たされているなら、インタラクトできるようにする
        if (loopRoomConditionControl.isGetBone)
        {
            triggerCollider.enabled = true;
            interactionObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPermitGave)
        {
            if (other.gameObject.tag == "Food")
            {
                isPermitGave = false;
                GiveBone();
                playerGetHandBone.SetActive(false);
                keyItem.SetActive(true);
            }
        }
    }

    private void GiveBone()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(CallGiveBone, 0f, true));
    }

    private void CallGiveBone()
    {
        dogTransform.position = keyItemPoint.position;
        dogTransform.rotation = keyItemPoint.rotation;
        dogAudioSource.PlayOneShot(seDogBarck);
    }

    public void CallGivePreparationEvent()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(PreparationGiveBone, 0, true));
    }

    private void PreparationGiveBone()
    {
        StartCoroutine(CorPreparation());
    }

    private IEnumerator CorPreparation()
    {
        playerTransform.position = prePlayerPoint.position;
        playerTransform.rotation = prePlayerPoint.rotation;

        dogTransform.position = preDogPoint.position;
        dogTransform.rotation = preDogPoint.rotation;

        playerGetHandBone.SetActive(true);

        yield return new WaitForSeconds(preparationDelay);
        isPermitGave = true;

        yield break;
    }
}
