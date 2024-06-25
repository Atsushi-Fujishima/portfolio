using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EventEdible : MonoBehaviour
{
    [SerializeField] private bool isPermit = true;

    [Header("Cry")]
    [SerializeField] AudioSource cryAudio;
    [SerializeField] private float cryTime = 8.0f;
    private bool isCryEnd = false;
    [Header("Knock")]
    [SerializeField] BathroomController bathroomController;
    [SerializeField] SoundEffectNaturalExpression knockController;
    [SerializeField] private float shortestDelay = 0.8f;
    [SerializeField] private float longestDelay = 1.0f;
    private bool isKnockEnd = false;
    [Header("Eating")]
    [SerializeField] AudioSource eatingAudio;
    [SerializeField] private float eatDelay = 3.0f;
    [SerializeField] private float eatTime = 15.0f;
    private bool isEatEnd = false;
    [Header("Nakutatte ii")]
    [SerializeField] HorrorEffectController[] horrorEffectControllers;

    private void Start()
    {
        EventSchedule();
        bathroomController.KnockDoor();
    }

    private void Update()
    {
        if (isPermit) EventObservation();
    }

    private void EventObservation()
    {
        if (isCryEnd && isKnockEnd && isEatEnd)
        {
            isPermit = false;
            // Permission to perform fear production
            StartHorrorEffect();
        }
    }

    private void EventSchedule()
    {
        StartCoroutine(CryControl());
        StartCoroutine(KnockControl());
        StartCoroutine(EatingControl());
    }

    private IEnumerator CryControl()
    {
        cryAudio.Play();
        yield return new WaitForSeconds(cryTime);

        isKnockEnd = true;

        while (cryAudio.volume > 0.01f)
        {
            cryAudio.volume -= 0.5f * Time.deltaTime;
            yield return null;
        }

        cryAudio.volume = 0;
        cryAudio.Stop();
        isCryEnd = true;
        yield break;
    }

    private IEnumerator KnockControl()
    {
        while (isKnockEnd == false)
        {
            knockController.PlaySound();
            var delay = Random.Range(shortestDelay, longestDelay);
            yield return new WaitForSeconds(delay);
        }


        bathroomController.CloseDoor();
        yield break;
    }

    private IEnumerator EatingControl()
    {
        yield return new WaitUntil(() => isCryEnd);

        yield return new WaitForSeconds(eatDelay);

        eatingAudio.Play();

        yield return new WaitForSeconds(eatTime);

        eatingAudio.loop = false;

        yield return new WaitWhile(() => eatingAudio.isPlaying);

        isEatEnd = true;
        yield break;
    }

    private void StartHorrorEffect()
    {
        if (horrorEffectControllers.Length == 0)
        {
            return;
        }
        else
        {
            foreach (var controller in horrorEffectControllers)
            {
                controller.CallEffect();
            }
        }
    }
}
