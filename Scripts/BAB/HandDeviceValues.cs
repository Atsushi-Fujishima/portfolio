using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandDeviceValues : MonoBehaviour
{
    [SerializeField] InputActionReference inputLeftHandPosition;
    [SerializeField] InputActionReference inputRightHandPosition;

    public static HandDeviceValues instance = null;
    private Vector3 leftHandPosition = Vector3.zero;
    private Vector3 rightHandPosition = Vector3.zero;
    private Vector3 leftPreviousHandPosition = Vector3.zero;
    private Vector3 rightPreviousHandPosition = Vector3.zero;
    private Vector3 lHandDirectionMovement = Vector3.zero;
    private Vector3 rHandDirectionMovement = Vector3.zero;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        leftPreviousHandPosition = inputLeftHandPosition.action.ReadValue<Vector3>();
        rightPreviousHandPosition = inputRightHandPosition.action.ReadValue<Vector3>();
    }

    private void Update()
    {
        //set position
        leftHandPosition = inputLeftHandPosition.action.ReadValue<Vector3>();
        rightHandPosition = inputRightHandPosition.action.ReadValue<Vector3>();

        //set direction of movement
        lHandDirectionMovement = (leftHandPosition - leftPreviousHandPosition).normalized;
        rHandDirectionMovement = (rightHandPosition - rightPreviousHandPosition).normalized;

        //se previous position
        leftPreviousHandPosition = leftHandPosition;
        rightPreviousHandPosition = rightHandPosition;
    }

    public Vector3 LeftHandPosition()
    {
        return leftHandPosition;
    }

    public Vector3 RightHandPosition()
    {
        return rightHandPosition;
    }

    public Vector3 LeftHandDirectionMovement()
    {
        return lHandDirectionMovement;
    }

    public Vector3 RightHandDirectionMovement()
    {
        return rHandDirectionMovement;
    }
}
