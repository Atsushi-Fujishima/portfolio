using Mono.CompilerServices.SymbolWriter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultMoveController : MonoBehaviour
{
    public enum MoveType { Target, Up, Down};
    public MoveType moveType = MoveType.Up;
    [Header("Target Move")]
    [SerializeField] Transform destinationPoint;
    [SerializeField] float destinationMoveSpeed = 1.5f;
    [SerializeField] float destinationMoveEndDistance = 1.0f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float rotaionThreshold = 0.1f;
    [Header("Up Move")]
    [SerializeField] float upHeight = 0f;
    [SerializeField] float upMoveSpeed = 1.0f;
    [SerializeField] float upMoveEndDistance = 0.1f;
    [Header("Down Move")]
    [SerializeField] float downHeight = 0f;
    [SerializeField] float downMoveSpeed = 1.0f;
    [SerializeField] float downMoveEndDistance = 1.0f;

    private Transform mTransform;
    private float moveSpeed = 0f;
    private float moveEndDistance = 0f;
    private Vector3 moveTargetPosition = Vector3.zero;
    private bool isTargetMove = false;
    private bool isUpMove = false;
    private bool isDownMove = false;
    private bool isRotation = false;

    private void Start()
    {
        mTransform = transform;
    }

    public void CallPlayMove(int typeIndex)
    {
        if (typeIndex == 0)
        {
            PlayMove(MoveType.Target);
        }
        else if (typeIndex == 1)
        {
            PlayMove(MoveType.Up);
        }
        else
        {
            PlayMove(MoveType.Down);
        }
    }

    public void PlayMove(MoveType moveType)
    {
        switch (moveType)
        {
            case MoveType.Target: SetTarget(); break;
            case MoveType.Up: SetUp(); break;
            case MoveType.Down: SetDown(); break;
        }
    }

    private void Update()
    {
        if (isTargetMove) TargetMoveControl();
        if (isUpMove) UpMoveControl();
        if (isDownMove) DownMoveControl();
    }

    private void TargetMoveControl()
    {
        if (IsMove())
        {
            mTransform.position = Vector3.MoveTowards(mTransform.position, moveTargetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            isTargetMove = false;
        }

        if (isRotation)
        {
            Vector3 directionTotarget = (moveTargetPosition - mTransform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionTotarget, Vector3.up);
            mTransform.rotation = Quaternion.Slerp(mTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            if (Quaternion.Angle(mTransform.rotation, targetRotation) < rotaionThreshold)
            {
                isRotation = false;
            }
        }
        
    }

    private void UpMoveControl()
    {
        if (IsMove())
        {
            mTransform.position = Vector3.MoveTowards(mTransform.position, moveTargetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            isUpMove = false;
        }
    }

    private void DownMoveControl()
    {
        if (IsMove())
        {
            mTransform.position = Vector3.MoveTowards(mTransform.position, moveTargetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            isDownMove = false;
        }
    }

    private void SetTarget()
    {
        moveSpeed = destinationMoveSpeed;
        moveEndDistance = destinationMoveEndDistance;
        moveTargetPosition = destinationPoint.position;
        StartCoroutine(MoveDelay());
    }

    private IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(2);
        isTargetMove = true;
        isRotation = true;
        yield break;
    }

    private void SetUp()
    {
        moveSpeed = upMoveSpeed;
        moveEndDistance = upMoveEndDistance;
        moveTargetPosition = new Vector3(mTransform.position.x, upHeight, mTransform.position.z);
        isUpMove = true;
    }

    private void SetDown()
    {
        moveSpeed = downMoveSpeed;
        moveEndDistance = downMoveEndDistance;
        moveTargetPosition = new Vector3(mTransform.position.x, downHeight, mTransform.position.z);
        isDownMove = true;
    }

    private bool IsMove()
    {
        var offset = moveTargetPosition - mTransform.position;
        var length = offset.sqrMagnitude;
        if (length > moveEndDistance * moveEndDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MoveInitialized()
    {
        isTargetMove = false;
        isUpMove = false;
        isDownMove = false;
        isRotation = false;
    }
}
