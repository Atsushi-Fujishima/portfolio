using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopPropsMover : MonoBehaviour
{
    [Header("Trigger Loop")]
    [SerializeField] GameObject targetLoop;
    [Header("Set Position")]
    [SerializeField] Vector3 setPosition;
    private bool isOneced = false;

    private void Update()
    {
        if (isOneced == false)
        {
            if (targetLoop.activeSelf)
            {
                isOneced = true;
                transform.localPosition = setPosition;
            }
        }
    }
}
