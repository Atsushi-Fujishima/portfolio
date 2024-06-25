using UnityEngine;

public class BabyFoundController : MonoBehaviour
{
    [SerializeField] AudioSource mAduio;
    [SerializeField] AudioClip mClip;
    public float babVolume = 0.5f;
    [SerializeField] GameObject babyPrefab;
    [Header("PlayerSetting")]
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform setPoint;
    [SerializeField] Transform[] hands;
    [SerializeField] PlayerInteractedManager interactedManager;

    private EventItemTips mTips;
    private UndoEventItemController eventItemController;

    private void Start()
    {
        eventItemController = GetComponent<UndoEventItemController>();

        if (GetComponent<EventItemTips>() != null)
        {
            mTips = GetComponent<EventItemTips>();
        }
        else
        {
            mTips = null;
        }
        
    }

    public void Interaction()
    {
        if (interactedManager != null)
        {
            interactedManager.Interacted();
        }

        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(FoundBaby, 0, true));
    }

    private void FoundBaby()
    {
        eventItemController.UndoEventItem();

        mAduio.PlayOneShot(mClip, babVolume);
        babyPrefab.SetActive(true);

        playerTransform.position = new Vector3(
            setPoint.position.x,
            playerTransform.position.y,
            setPoint.position.z);
        playerTransform.rotation = setPoint.rotation;

        foreach (Transform hand in hands)
        {
            hand.position = playerTransform.position;
        }

        if (mTips != null) mTips.DeleteTips();
    }
}
