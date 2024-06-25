using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB_SisterController : MonoBehaviour
{
    [SerializeField] Transform playerCharacterTransform;
    [SerializeField] LB_SisterDestroyHidePoint sisterDestroyHidePoint;
    private Transform mTransform;
    [Header("Status")]
    public bool isMove = true;
    public bool isRotate = true;
    public string[] moveStates = { "Wandering", "Chase", "HidePoint" };
    [SerializeField] private string currentMoveState = string.Empty;
    [Header("Move")]
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] float closeDistance = 2.0f;
    private float setMoveSpeed = 0f;
    private int currentRouteIndex = 0;
    [SerializeField] Transform[] routes;
    [SerializeField] Transform initializePoint;
    [Header("Rotate")]
    [SerializeField] float rotationSpeed = 2.0f;
    [Header("Approach HidePoint")]
    [SerializeField] float hpCloseDistance = 1.5f;
    [Header("Vigilance Turn")]
    [SerializeField] int vigilanceTurnRouteIndex = 0;
    [SerializeField] float turnSpeed = 0f;
    [SerializeField] float turnMoveSpeed = 0f;
    [SerializeField] float turnPausedTime = 2.0f;
    private float pausedElapsed = 0f;
    private bool isPermitVigilanceTurn = true;
    private bool isVigilanceTurn = false;
    private bool isVigilanceTurnPaused = false;
    private float turnAmount = 0f;

    private Quaternion targetRotation = Quaternion.identity;
    private HidePointController targetHidePoint = null;

    private void Start()
    {
        mTransform = transform;
        InitializeRoutesHight();
        InitializeMove();
    }

    private void Update()
    {
        if (isMove) Move(); 
        if (isRotate) RotaionUpdate();
        if (isVigilanceTurn) VigilanceTurn();
        if (isVigilanceTurnPaused) VigilanceTurnPausedControl();
    }

    private void Move()
    {
        if (currentMoveState == moveStates[0])
        {
            if (currentRouteIndex == vigilanceTurnRouteIndex)
            {
                if (isPermitVigilanceTurn)
                {
                    SetUpVigilanceTurn();
                }
            }

            WanderMove();
        }
        else if (currentMoveState == moveStates[1])
        {
            if (isVigilanceTurn)
            {
                EndVigilanceTurn();
            }

            ChaseMove();
        }
        else
        {
            if (isVigilanceTurn)
            {
                EndVigilanceTurn();
            }
            
            HidePointMove();
        }
    }

    private void WanderMove()
    {
        if (GetDistance(routes[currentRouteIndex].position, mTransform.position) < closeDistance * closeDistance)
        {
            SetNextRoute();
        }
        else
        {
            mTransform.position = Vector3.MoveTowards(mTransform.position, routes[currentRouteIndex].position, Time.deltaTime * setMoveSpeed);
        }
    }

    private void ChaseMove()
    {
        mTransform.position = Vector3.MoveTowards(mTransform.position,
            new Vector3(
                playerCharacterTransform.position.x,
                mTransform.position.y,
                playerCharacterTransform.position.z),
            Time.deltaTime * setMoveSpeed);

        SetTargetRotation(playerCharacterTransform);
    }

    private void HidePointMove()
    {
        if (targetHidePoint == null) return;

        var targetPosition = targetHidePoint.GetTransform().position;
        
        if (GetDistance(targetPosition, mTransform.position) < hpCloseDistance * hpCloseDistance)
        {
            // break hide point move. please stop sister.
            MyStaticMethod.DisplayColorLog("c", "break hide point move. please stop sister.", "", "");
            isMove = false; // is stop
            sisterDestroyHidePoint.DestroySelectedHidePoint(targetHidePoint);
            targetHidePoint = null; // initialize
        }
        else
        {
            mTransform.position = Vector3.MoveTowards(mTransform.position, targetPosition, Time.deltaTime * setMoveSpeed);
        }
    }

    private void VigilanceTurn()
    {
        if (isVigilanceTurnPaused)
            return;

        var rotationAmount = turnSpeed * Time.deltaTime;
        turnAmount += rotationAmount;
        
        if (turnAmount > 360f)
        {
            EndVigilanceTurn();
        }
        else 
        {
            if (turnAmount >= 180f && turnAmount - rotationAmount < 180f) // If it was less than 180 degrees in the previous frame, and it becomes 180 degrees or more in the current frame.
            {
                isVigilanceTurnPaused = true;
            }

            Vector3 currentRotation = mTransform.rotation.eulerAngles;
            currentRotation += Vector3.up * rotationAmount;
            mTransform.rotation = Quaternion.Euler(currentRotation);
        }
    }

    private void VigilanceTurnPausedControl()
    {
        pausedElapsed += Time.deltaTime;
        if (pausedElapsed > turnPausedTime)
        {
            pausedElapsed = 0f;
            isVigilanceTurnPaused = false;
        }
    }

    private void SetUpVigilanceTurn()
    {
        isPermitVigilanceTurn = false;
        isRotate = false;
        isVigilanceTurn = true;
        setMoveSpeed = turnMoveSpeed;
    }

    private void EndVigilanceTurn()
    {
        isVigilanceTurn = false;
        isRotate = true;
        turnAmount = 0f;
        setMoveSpeed = moveSpeed;
        isVigilanceTurnPaused = false;
        pausedElapsed = 0f;
    }

    private void SetNextRoute()
    {
        currentRouteIndex++;
        currentRouteIndex = (currentRouteIndex < routes.Length) ? currentRouteIndex : 0;
        SetTargetRotation(routes[currentRouteIndex]);

        if (currentRouteIndex == 0)
        {
            isPermitVigilanceTurn = true; // initialize the vigilance turn flag.
        }
    }

    private float GetDistance(Vector3 _other, Vector3 _this)
    {
        var offset = _other - _this;
        return offset.sqrMagnitude;
    }

    private void SetTargetRotation(Transform _target)
    {
        var directionToTarget = _target.position - transform.position;
        targetRotation = Quaternion.LookRotation(directionToTarget);
        targetRotation.eulerAngles = new Vector3(0, targetRotation.eulerAngles.y, 0);
    }

    private void RotaionUpdate()
    {
        mTransform.rotation = Quaternion.Slerp(mTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void InitializeRoutesHight()
    {
        foreach (var route in routes)
        {
            route.position = new Vector3(route.position.x, mTransform.position.y, route.position.z);
        }

        initializePoint.position = new Vector3(initializePoint.position.x, mTransform.position.y, initializePoint.position.z); 
    }

    public void SetMoveState(string _state)
    {
        if (_state == moveStates[0])
        {
            SetTargetRotation(routes[currentRouteIndex]);
        }
        else if (_state == moveStates[1])
        {
            SetTargetRotation(playerCharacterTransform);
        }
        else
        {
            SetTargetRotation(targetHidePoint.GetTransform());
        }

        currentMoveState = _state;
    }

    public string GetMoveState()
    {
        return currentMoveState;
    }

    public void InitializeMove()
    {
        mTransform.position = initializePoint.position;
        currentRouteIndex = 0;
        SetMoveState(moveStates[0]);
        isMove = true;
        setMoveSpeed = moveSpeed;
        isPermitVigilanceTurn = true;
        isVigilanceTurn = false;
        isRotate = true;
        turnAmount = 0f;
        pausedElapsed = 0f;
        isVigilanceTurnPaused = false;
    }

    public void ReStartMove()
    {
        SetMoveState(moveStates[0]);
        isMove = true;
        MyStaticMethod.DisplayColorLog("c", "ReStart Move", "", "");
    }

    public void StopMove()
    {
        isMove = false;
    }

    public void SetHidePoint(HidePointController _hp)
    {
        targetHidePoint = _hp;
        MyStaticMethod.DisplayColorLog("c", "SisterController", "set hide point", targetHidePoint.gameObject.name);
    }
}
