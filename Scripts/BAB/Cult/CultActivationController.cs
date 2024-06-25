using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultActivationController : MonoBehaviour
{
    [SerializeField] CultAnimationController animationController;
    [SerializeField] CultSoundController soundController;

    private void OnEnable()
    {
        animationController.PlayAnimation(CultAnimationController.AnimType.Idle);
        soundController.PlayCultSound();
    }

    
}
