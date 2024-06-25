using UnityEngine;

public class HorrorEventFootstepsController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] SoundEffectNaturalExpression soundEffect;
    [SerializeField] AudioSource audioSource;
    [SerializeField] PlayerMonitorRotationAndTurn playerMonitor;
    [Header("Move Settings")]
    public float walkSpeed = 0.5f;
    public float runSpeed = 1.0f;
    public float runTime = 3.0f;
    public float stopedDistance = 1.5f;
    [Header("Sound Settings")]
    public float walkSoundCycleMin = 0.8f;
    public float walkSoundCycleMax = 1.0f;
    public float runSoundCycle = 0.2f;
    
    private Transform mTransform;
    private bool isMove = false;
    private float currentSpeed = 0.0f;
    private float footstepsSoundCycle = 0f;
    private float elapsedMoveTime = 0.0f;
    private float elapsedSoundTime = 0.0f;
    private bool isRun = false;

    private void Start()
    {
        mTransform = transform;
        currentSpeed = walkSpeed;
        footstepsSoundCycle = walkSoundCycleMax;
    }

    private void Update()
    {
        if (isMove)
        {
            if (playerMonitor.GetIsTurn())
            {
                isMove = false;
            }

            if (isRun == false)
            {
                MoveTimeCount();
            }
            else
            {
                if (currentSpeed < runSpeed)
                {
                    currentSpeed += walkSpeed * Time.deltaTime;
                }
            }

            GetCloser();
            SoundControl();
        }
    }

    private void MoveTimeCount()
    {
        elapsedMoveTime += Time.deltaTime;
        if (elapsedMoveTime > runTime)
        {
            isRun = true;
            elapsedMoveTime = 0.0f;
        }
    }

    private void GetCloser()
    {
        if (GetDistance() > stopedDistance * stopedDistance)
        {
            mTransform.position = Vector3.MoveTowards(mTransform.position, target.position, currentSpeed * Time.deltaTime);
        }
        else
        {
            isMove = false;
        }
    }

    public void StartMove()
    {
        float distance = Vector3.Distance(mTransform.position, target.position);
        audioSource.maxDistance = distance;
        isMove = true;
    }

    private float GetDistance()
    {
        var offset = target.position - mTransform.position;
        var sqrLength = offset.sqrMagnitude;
        return sqrLength;
    }

    private void SoundControl()
    {
        elapsedSoundTime += Time.deltaTime;
        if (elapsedSoundTime > footstepsSoundCycle)
        {
            elapsedSoundTime = 0f;
            
            soundEffect.PlaySound();

            if (isRun == false)
            {
                footstepsSoundCycle = Random.Range(walkSoundCycleMin, walkSoundCycleMax);
            }
            else
            {
                if (footstepsSoundCycle != runSoundCycle)
                    footstepsSoundCycle = runSoundCycle;
            }
        }
    }
}
