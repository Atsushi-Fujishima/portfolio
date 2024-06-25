using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStatuesController : MonoBehaviour
{
    public Transform targetTransform;
    [SerializeField] ObjectVisible visible;
    [Header("Statues Settings")]
    [SerializeField] Vector3 returnPosition = Vector3.zero;
    [SerializeField] float changeValue = 0.5f;
    [SerializeField] bool isHorizontal = false;
    [SerializeField] bool isVertical = false;
    [SerializeField] bool isDepth = false;
    private Vector3 defaultPosition = Vector3.zero;
    private Vector3 currentPosition = Vector3.zero;
    public float cycleTime = 0f;

    private bool isActiveSystem = true;

    private void Start()
    {
        defaultPosition = targetTransform.position;
        currentPosition = targetTransform.position;
    }

    private void Update()
    {
        if (isActiveSystem == false) return;

        if (visible.GetIsShow() == false)
        {
            cycleTime += Time.deltaTime;
            StatuesControl();
        }
    }

    private void StatuesControl()
    {
        if (isVertical)
        {
            currentPosition.y += LimitedGrowth(returnPosition.y, currentPosition.y);
        }
        else
        {
            currentPosition.y = defaultPosition.y;
        }

        if (isHorizontal)
        {
            currentPosition.x += LimitedGrowth(returnPosition.x, currentPosition.x);
        }
        else
        {
            currentPosition.x = defaultPosition.x;
        }

        if (isDepth)
        {
            currentPosition.z += LimitedGrowth(returnPosition.z, currentPosition.z);
        }
        else
        {
            currentPosition.z = defaultPosition.z;
        }

        targetTransform.position = currentPosition;
    }

    private float CyclicInflation(float defaultValue, float endValue, float time)
    {
        float t = Mathf.Sin(time * changeValue) * 0.5f + 0.5f;
        var currentValue = Mathf.Lerp(defaultValue, endValue, t);
        return currentValue;
    }

    private float LimitedGrowth(float limitValue, float currentValue)
    {
        float newValue;

        if (currentValue < limitValue)
        {
            newValue = Time.deltaTime * changeValue;
            return newValue;
        }
        else
        {
            newValue = 0f;
            return newValue;
        }
    }

    public void DisableStatuesControl()
    {
        isActiveSystem = false;
    }
}
