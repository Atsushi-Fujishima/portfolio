using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDecDollController : MonoBehaviour
{
    [SerializeField] GazeController gazeController;
    [SerializeField] TakeGazeController takeGazeController;
    [SerializeField] Animator animator;
    public float setRayDistance = 20.0f;

    public void DollInteraction()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(DollAwake, 0, true));
    }

    private void DollAwake()
    {
        animator.SetBool("Awake", true);
        gazeController.rayDistance = setRayDistance;
        takeGazeController.EnableGazeTrigger();
    }
}
