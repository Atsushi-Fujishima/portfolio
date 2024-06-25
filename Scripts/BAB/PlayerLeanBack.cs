using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLeanBack : MonoBehaviour
{
    static public PlayerLeanBack instance;
    public bool isLeanBack = false;
    public float rotationAmount = -50.0f;
    public float rotationSpeed = 2.0f;
    public float delay = 1.0f;
    public Transform targetTransform = null;
    private Quaternion targetRotation = Quaternion.identity;
    private Quaternion leanBackRotation;
    private IEnumerator cacheCor = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        targetRotation = Quaternion.Euler(0, -90f, 0);
        leanBackRotation = Quaternion.Euler(rotationAmount, -90f, 0);
    }

    public void CallLeanBack()
    {
        if (cacheCor != null) StopCoroutine(cacheCor);
        cacheCor = null;
        cacheCor = LeanBack();
        StartCoroutine(cacheCor);
    }

    private IEnumerator LeanBack()
    {
        // rotate to target angle
        targetTransform.rotation = leanBackRotation;
        // delay
        yield return new WaitForSeconds(delay);

        // calculate the difference from the target rotation
        float rotationDiff = Mathf.DeltaAngle(0f, targetTransform.localEulerAngles.x);

        // update
        while (Mathf.Abs(rotationDiff) > 0.01f)
        {
            // rotate
            targetTransform.rotation = Quaternion.Lerp(targetTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // recalculate rotation difference
            rotationDiff = Mathf.DeltaAngle(0f, targetTransform.localEulerAngles.x);

            yield return null;
        }

        // initialize
        targetTransform.rotation = targetRotation;

        cacheCor = null;
        yield break;
    }
}
