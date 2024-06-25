using UnityEngine;

public class VirtualButtonController : MonoBehaviour
{
    [SerializeField] MenuSceneController menuSceneController;
    [SerializeField] HandHapticManager handHapticManager;
    public string[] handTags = { "LeftHand", "RightHand" };
    public int callMenuStepNumber = 0;
    public GameObject displayGroup;
    [Header("Touch Effects")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    [SerializeField] ParticleSystem touchEffect;

    private bool isTouched = false;
    private BoxCollider thisCollider;
    private bool isFirstLateUpdated = false;

    private void Start()
    {
        thisCollider = GetComponent<BoxCollider>();
    }

    private void LateUpdate()
    {
        if (isFirstLateUpdated == false) isFirstLateUpdated = true; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTouched || isFirstLateUpdated == false) return;
        string triggerObjectTag = other.gameObject.tag;
        if (triggerObjectTag.Contains(handTags[0])|| triggerObjectTag.Contains(handTags[1]))
        {
            HapticHandControl(triggerObjectTag);
            TouchVirtualButton();
        }
    }

    private void TouchVirtualButton()
    {
        isTouched = true;
        thisCollider.enabled = false;
        menuSceneController.TouchButtonControl(callMenuStepNumber);
        TouchEffect();
    }

    private void TouchEffect()
    {
        displayGroup.SetActive(false);
        audioSource.PlayOneShot(audioClip);
        touchEffect.Play();
    }

    private void HapticHandControl(string handType)
    {
        if (handType == handTags[0])
        {
            // left
            handHapticManager.LeftHandHaptic(0.5f, 0.5f);
        }
        else
        {
            // right
            handHapticManager.RightHandHaptic(0.5f, 0.5f);
        }
    }
}
