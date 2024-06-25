using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivatationController : MonoBehaviour
{
    [Header("Settings")]
    public bool changePosition = false;
    public bool changeRotaion = false;
    public bool changeScale = false;
    public bool changeActive = false;
    [Header("Set Value")]
    public Vector3 setPosition = Vector3.zero;
    public Vector3 setRotation = Vector3.zero;
    public Vector3 setScale = Vector3.zero;
    [Header("Only \"Change Active\" is Valid")]
    public GameObject target;

    private Vector3 initializePosition = Vector3.zero;
    private Vector3 initializeRotation = Vector3.zero;
    private Vector3 initializeScale = Vector3.zero;

    private void Start()
    {
        initializePosition = transform.localPosition;
        initializeRotation = transform.localEulerAngles;
        initializeScale = transform.localScale;
    }

    public void Activate()
    {
        if (changePosition) transform.localPosition = setPosition;
        if (changeRotaion) transform.localRotation = Quaternion.Euler(setRotation);
        if (changeScale) transform.localScale = setScale;
        if (changeActive) target.SetActive(true);
    }

    public void CalmDown()
    {
        if (changePosition) transform.localPosition = initializePosition;
        if (changeRotaion) transform.localEulerAngles = initializeRotation;
        if (changeScale) transform.localScale = initializeScale;
        if (changeActive) 
        { 
            if (target.activeSelf) target.SetActive(false);
        }
    }
}
