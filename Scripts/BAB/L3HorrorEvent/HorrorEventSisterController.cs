using UnityEngine;
using UnityEngine.InputSystem;

public class HorrorEventSisterController : MonoBehaviour
{
    public GameObject meshGroup;
    [SerializeField] Transform playerTransform;
    [Header("Look")]
    [SerializeField] Transform sisterHead;
    [SerializeField] float lookSpeed = 0.5f;
    [SerializeField] float rotationThresholdAngle = 0.1f;
    public Vector3 front = Vector3.forward;
    [Header("Move")]
    public float hideSpeed = 0.5f;
    [Header("Attack")]
    [SerializeField] Vector3 setPosition = Vector3.zero;
    [SerializeField] Animator mAnimator;
    [SerializeField] AudioSource mAudioSource;
    public float attackSpeed = 5.0f;
    private Vector3 targetDirection = Vector3.zero;

    private Transform mTransform;
    private float speed = 0f;
    private bool isLookPlayer = false;
    private bool isAttack = false;
    private bool isLookEnd = false;
    private Quaternion saveHeadRotation = Quaternion.identity;

    private void Start()
    {
        speed = hideSpeed;
        mTransform = transform;
        saveHeadRotation = sisterHead.rotation;
    }

    private void Update()
    {
        if (isLookPlayer)
        {
            LookPlayer();
        }

        if (isAttack)
        {
            Attack();
        }
    }

    private void LookPlayer()
    {
        Vector3 direction = playerTransform.position - sisterHead.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        Quaternion offsetRotaiton = Quaternion.FromToRotation(front, Vector3.forward);
        sisterHead.rotation = Quaternion.Lerp(sisterHead.rotation, lookRotation * offsetRotaiton, Time.deltaTime * lookSpeed);

        if (IsRotationComplete(sisterHead.rotation, lookRotation * offsetRotaiton))
        {
            isLookPlayer = false;
            isLookEnd = true;
        }
    }

    private bool IsRotationComplete(Quaternion currentRotation, Quaternion targetRotation)
    {
        // 回転の差異を計算し、それが閾値以下であるかどうかを確認します
        float angleDifference = Quaternion.Angle(currentRotation, targetRotation);
        return angleDifference < rotationThresholdAngle;
    }

    public void HiddenSister()
    {
        meshGroup.SetActive(false);
        sisterHead.rotation = saveHeadRotation;
    }

    private void Attack()
    {
        mTransform.position += targetDirection * speed * Time.deltaTime;
    }

    public void LookStart()
    {
        isLookPlayer = true;
    }

    public void StartAttack()
    {
        meshGroup.SetActive(true);
        speed = attackSpeed;
        mTransform.position = setPosition;
        mAnimator.enabled = true;
        mTransform.LookAt(playerTransform);
        mAudioSource.Play();
        targetDirection = (playerTransform.position - mTransform.position).normalized;
        isAttack = true;
    }

    public bool GetEndLook()
    {
        return isLookEnd;
    }
}
