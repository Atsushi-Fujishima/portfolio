using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectDestructionControl : MonoBehaviour
{
    public float destroyDelay = 1.0f;
    [Header("Loop 0")]
    [SerializeField] GameObject l0_destroySceneObject;
    [SerializeField] GameObject l0_targetFlagSceneObject;
    private bool isL0Destroy = false;
    [Header("Loop 1")]
    [SerializeField] GameObject l1_destroySceneObject;
    [SerializeField] GameObject l1_targetFlagSceneObject;
    private bool isL1Destroy = false;
    [Header("Loop 2")]
    [SerializeField] GameObject l2_destroySceneObject;
    [SerializeField] GameObject l2_targetFlagSceneObject;
    private bool isL2Destroy = false;
    [Header("Loop 3")]
    [SerializeField] GameObject l3_destroySceneObject;
    [SerializeField] GameObject l3_targetFlagSceneObject;
    private bool isL3Destroy = false;
    [Header("Loop 4")]
    [SerializeField] GameObject l4_destroySceneObject;
    [SerializeField] GameObject l4_targetFlagSceneObject;
    private bool isL4Destroy = false;
    [Header("Loop 5")]
    [SerializeField] GameObject l5_destroySceneObject;
    [SerializeField] GameObject l5_targetFlagSceneObject;
    private bool isL5Destroy = false;

    private void Update()
    {
        DestroyControl();
    }

    private void DestroyControl()
    {
        if (isL0Destroy == false)
        {
            if (l0_targetFlagSceneObject.activeSelf)
            {
                isL0Destroy = true;
                ExecuteDiscard(l0_destroySceneObject);
            }
        }

        if (isL1Destroy == false)
        {
            if (l1_targetFlagSceneObject.activeSelf)
            {
                isL1Destroy = true;
                ExecuteDiscard(l1_destroySceneObject);
            }
        }

        if (isL2Destroy == false)
        {
            if (l2_targetFlagSceneObject.activeSelf)
            {
                isL2Destroy = true;
                ExecuteDiscard(l2_destroySceneObject);
            }
        }

        if (isL3Destroy == false)
        {
            if (l3_targetFlagSceneObject.activeSelf)
            {
                isL3Destroy = true;
                ExecuteDiscard(l3_destroySceneObject);
            }
        }

        if (isL4Destroy == false)
        {
            if (l4_targetFlagSceneObject.activeSelf)
            {
                isL4Destroy = true;
                ExecuteDiscard(l4_destroySceneObject);
            }
        }

        if (isL5Destroy == false)
        {
            if (l5_targetFlagSceneObject.activeSelf)
            {
                isL5Destroy = true;
                ExecuteDiscard(l5_destroySceneObject);
            }
        }
    }

    private void ExecuteDiscard(GameObject destroyGameObject)
    {
        StartCoroutine(CorExecuteDestroy(destroyGameObject));
    }

    private IEnumerator CorExecuteDestroy(GameObject _destroyGameObject)
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(_destroyGameObject);
        yield break;
    }
}
