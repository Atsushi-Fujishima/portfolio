using UnityEngine;

public class LB_LastSisterMoveController : MonoBehaviour
{
    public bool isChase = false;
    [SerializeField] Transform targetTransform;
    [SerializeField] PlayerMonitorRotationAndTurn playerMonitor;
    [SerializeField] Transform initializePoint;
    [Header("Value")]
    [SerializeField] float startSpeed = 0.2f;
    [SerializeField] float defaultSpeed = 3.0f;
    [SerializeField] float topSpeed = 6.0f;
    [SerializeField] float rotationSpeed = 1.5f;
    [SerializeField] float autoRunDelay = 5.0f;
    [Header("Distance")]
    [SerializeField] float nearDistance = 3.0f;
    
    private float setSpeed = 0f;
    private bool isStandby = true;
    private Transform mTransform;
    private float elapsedTime = 0f;
    private bool isFirst = true;
    
    private void Start()
    {
        setSpeed = startSpeed;
        mTransform = transform;
        mTransform.position = initializePoint.position;
    }

    private void OnEnable()
    {
        isChase = true;
    }

    private void Update()
    {
        if (isChase)
        {
            Chase();
        }

        if (isFirst)
        {
            AutoAcceleration();
        }
    }

    private void AutoAcceleration()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > autoRunDelay)
        {
            isFirst = false;
            setSpeed = topSpeed;
            elapsedTime = 0f;
        }
    }

    private void Chase()
    {
        // If the player is turning around, increase the speed
        if (playerMonitor.GetIsTurn())
        {
            if (isStandby == false) setSpeed = topSpeed;
        }

        // If close to target, keep speed at default
        if (DistanceForTarget(targetTransform.position, mTransform.position) < nearDistance * nearDistance)
        {
            setSpeed = defaultSpeed;
        }

        // move
        mTransform.position = Vector3.MoveTowards(mTransform.position,
            new Vector3(targetTransform.position.x, mTransform.position.y, mTransform.position.z), Time.deltaTime * setSpeed);

        // rotate
        var directionToTarget = targetTransform.position - mTransform.position;
        var targetRotation = Quaternion.LookRotation(directionToTarget);
        targetRotation.eulerAngles = new Vector3(0, targetRotation.eulerAngles.y, 0);
        mTransform.rotation = Quaternion.Slerp(mTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private float DistanceForTarget(Vector3 _target, Vector3 _this)
    {
        var offset = _target - _this;
        return offset.sqrMagnitude;
    }

    public void SetDefaultSpeed()
    {
        if (setSpeed == topSpeed)
        {
            setSpeed = topSpeed;
        }
        else
        {
            setSpeed = defaultSpeed;
        }

        isStandby = false;
        isFirst = false;
    }

    public void LastSisterInitialize()
    {
        isChase = false;
        isStandby = true;
        isFirst = true;
        elapsedTime = 0f;
        setSpeed = startSpeed;
        mTransform.position = initializePoint.position;
    }
}