using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RattleController : MonoBehaviour
{
    public GameObject[] exclusionGameObjects;
    public Transform hand;
    [Header("Rattle")]
    [SerializeField] GameObject normalRattle;
    [SerializeField] GameObject penetrationRattle;
    [Header("Control")]
    [SerializeField] float bigSwing = 1.0f;
    [SerializeField] float smallSwing = 0.5f;
    [SerializeField] float disableSwing = 0.3f;
    [Header("Sound")]
    [SerializeField] SoundVolumeChanger volumeChanger;
    [SerializeField] SoundEffectNaturalExpression soundEffectNaturalExpression;

    private Vector3 previousPosition;
    private float sqrbigSwing;
    private float sqrbigSmallSwing;
    private float sqrbigDisableSwing;
    private bool isPlaySound = true;

    private void Start()
    {
        sqrbigDisableSwing = disableSwing * disableSwing;
        sqrbigSwing = bigSwing * bigSwing;
        sqrbigSmallSwing = smallSwing * smallSwing;
        previousPosition = hand.position;
    }

    private void Update()
    {
        var offset = previousPosition - hand.position;
        var sqrLength = offset.sqrMagnitude;
        if (sqrLength > sqrbigSwing)
        {
            // dekai oto
            MyStaticMethod.DisplayColorLog("y", "Big", "Rattle SoundEffect", "");
            PlaySound("big");
        }
        else if (sqrLength < sqrbigSmallSwing)
        {
            if (sqrLength < sqrbigDisableSwing)
            {
                // oto nashi
                previousPosition = hand.position;
                return;
            }
            else
            {
                // tiisai oto
                PlaySound("small");
                MyStaticMethod.DisplayColorLog("b", "small", "Rattle SoundEffect", "");
            }
        }
        else
        {
            // hutuu
            PlaySound("normal");
            MyStaticMethod.DisplayColorLog("c", "Normal", "Rattle SoundEffect", "");
        }

        previousPosition = hand.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var target in exclusionGameObjects)
        {
            if (other == target)
            {
                return;
            }
        }

        normalRattle.SetActive(false);
        penetrationRattle.SetActive(true);
        isPlaySound = false;
    }

    private void OnTriggerExit(Collider other)
    {
        penetrationRattle.SetActive(false);
        normalRattle.SetActive(true);
        isPlaySound = true;
    }

    private void PlaySound(string soundName)
    {
        if (isPlaySound == false) return;

        if (soundName == "big")
        {
            volumeChanger.SetHighVolume();
        }
        else if (soundName == "small")
        {
            volumeChanger.SetLowVolume();
        }
        else
        {
            volumeChanger.SetDefaultVolume();
        }

        soundEffectNaturalExpression.PlaySound();
    }
}
