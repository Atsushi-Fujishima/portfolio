using UnityEngine;

public class GameGuideBabyMover : MonoBehaviour
{
    public enum RotationTiming { Beginning, Relay };
    [SerializeField] GameGuideController gameGuideController;
    [SerializeField] int babyCode = 0;
    [Header("Move Point")]
    [SerializeField] Transform relayPoint;
    [SerializeField] Transform targetPoint;
    [Header("Move Settings")]
    [SerializeField] float reachDistance = 0.5f;
    [SerializeField] float moveSpeed = 1.0f;
    [Header("Rotation Setting")]
    public RotationTiming timing;
    [SerializeField] float rotationSpeed = 5.0f;
    [Header("Idle Settings")]
    [SerializeField] float idleMotionSpeed = 1.0f;
    [SerializeField] float minHeight = 0f;
    [SerializeField] float maxHeight = 1.5f;
    [SerializeField] float cycleSpeed = 0.5f;
    [Header("Sound Effect")]
    [SerializeField] AudioClip clipPossesses;
    [Space]
    public bool isMove = false;
    private Transform mTransform;
    private AudioSource mSource;
    private bool isReachRelay = false;
    private float sqrReachDistance = 0; 
    private GameObject meshGameObject;
    private float displacementValue = 0;
    private float startTime = 0f;

    private void Start()
    {
        // get 
        mSource = GetComponent<AudioSource>();
        mTransform = transform;
        meshGameObject = mTransform.GetChild(0).gameObject;
        // set
        sqrReachDistance = reachDistance * reachDistance;
        // look relay point
        mTransform.LookAt(relayPoint.position);
        
        startTime = Time.time;
    }

    private void Update()
    {
        if (isMove) 
        {
            RotationBaby();
            MoveBaby();
        }
        else
        {
            IdleBaby();
        }
    }

    private void MoveBaby()
    {
        if (isReachRelay == false)
        {
            MoveToRelayPoint();
        }
        else
        {
            MoveToTargetPoint();
        }
    }

    // namerakana kaitenn
    private void RotationBaby()
    {
        if (timing == RotationTiming.Beginning)
        {
            RotationControl();
        }
        else
        {
            if (isReachRelay)
            {
                RotationControl();
            }
        }
        
    }

    private void MoveToRelayPoint()
    {
        if (IsReachToPoint(relayPoint.position))
        {
            // change destination
            isReachRelay = true;
        }
        else
        {
            // move to relay point
            MoveControl(relayPoint.position);
        }
    }

    private void MoveToTargetPoint()
    {
        if (IsReachToPoint(targetPoint.position))
        {
            // end move
            ReachTargetPoint();
        }
        else
        {
            // move to target point
            MoveControl(targetPoint.position);
        }
    }

    private void MoveControl(Vector3 _destination)
    {
        mTransform.position = Vector3.MoveTowards(mTransform.position, _destination, moveSpeed * Time.deltaTime);
    }

    private void RotationControl()
    {
        Vector3 direction = targetPoint.position - mTransform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        mTransform.rotation = Quaternion.Lerp(mTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void ReachTargetPoint()
    {
        isMove = false;
        mSource.PlayOneShot(clipPossesses);
        meshGameObject.SetActive(false);
        gameGuideController.BabyReachTargetPoint(babyCode);
    }

    private bool IsReachToPoint(Vector3 point)
    {
        var offset = mTransform.position - point;
        var sqrLength = offset.sqrMagnitude;
        if (sqrLength < sqrReachDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void IdleBaby()
    {
        float elapsedTime = Time.time - startTime;
        float cycle = Mathf.Sin(elapsedTime * cycleSpeed);
        float mappedValue = Mathf.Lerp(minHeight, maxHeight, (cycle + 1) / 2);

        displacementValue = Mathf.Lerp(mTransform.position.y, mappedValue, Time.deltaTime * idleMotionSpeed);

        mTransform.position = new Vector3(
            mTransform.position.x, displacementValue, mTransform.position.z);        
    }
}
