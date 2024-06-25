using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggerEventController : MonoBehaviour
{
    public enum TriggerType
    {
        Collision,
        Trigger
    }

    public enum TargetType
    {
        Body,
        Hand
    }

    [Header("State")]
    public bool isEnable = true;
    [Header("Setting")]
    public bool isOneced = true;
    public bool isNotHandVibration = false;
    [Header("Triiger Type")]
    public TriggerType triggerType = TriggerType.Collision;
    [Header("Conditions")]
    public TargetType targetType = TargetType.Body;
    [Header("Player Setting")]
    public string playerTag = "Player";
    public LayerMask handLayer;
    [Header("Events")]
    public UnityEvent OnCallEvent;

    private bool isBreak = false;
    private bool isPermit = true;
    private readonly string defaultTagName = "Untagged";
    private string interactTagName = "InteractObject";

    private void Start()
    {
        if (isNotHandVibration)
        {
            gameObject.tag = defaultTagName;
        }
    }

    private void Update()
    {
        if (isNotHandVibration == false)
            TagControl();
    }

    public void TagControl()
    {
        if (isBreak)
        {
            if (gameObject.CompareTag(defaultTagName) == false)
                gameObject.tag = defaultTagName;

            return;
        }
        
        if (isBreak == false && isEnable)
        {
            if (gameObject.CompareTag(interactTagName) == false)
                gameObject.tag = interactTagName;
        }
        else if (isBreak == false && isEnable == false)
        {
            if (gameObject.CompareTag(defaultTagName) == false)
                gameObject.tag = defaultTagName;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerType == TriggerType.Collision)
            return;

        if (isEnable == false)
            return;

        if (isBreak || isPermit == false)
            return;

        if (targetType == TargetType.Body && other.gameObject.CompareTag(playerTag))
        {
            OnCallEvent?.Invoke();
            if (isOneced == true) isBreak = true;

            isPermit = false;
            StartCoroutine(TimeWaitPermit());
        }
        else if (targetType == TargetType.Hand && (handLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            OnCallEvent?.Invoke();
            if (isOneced == true) isBreak = true;

            isPermit = false;
            StartCoroutine(TimeWaitPermit());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (triggerType == TriggerType.Trigger)
            return;

        if (isEnable == false) 
            return;

        if (isBreak || isPermit == false)
            return;

        if (targetType == TargetType.Body && other.gameObject.CompareTag(playerTag))
        {
            OnCallEvent?.Invoke();
            if (isOneced == true) isBreak = true;

            isPermit = false;
            StartCoroutine(TimeWaitPermit());
        }
        else if (targetType == TargetType.Hand && (handLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            GameObject hand = other.gameObject;
            if (hand.GetComponent<PlayerInteractTouch>() != null)
            {
                var touch = hand.GetComponent<PlayerInteractTouch>();
                if (touch.GetTouchFloor())
                {
                    return;
                }
            }

            OnCallEvent?.Invoke();
            if (isOneced == true) isBreak = true;

            isPermit = false;
            StartCoroutine(TimeWaitPermit());
        }
    }

    private IEnumerator TimeWaitPermit()
    {
        yield return new WaitForSeconds(3f);

        isPermit = true;

        yield break;
    }

    public bool IsBreak()
    {
        return isBreak;
    }

    public void DisableCallEventAction(int _index)
    {
        OnCallEvent.SetPersistentListenerState(_index, UnityEventCallState.Off);
    }

    public void EnableCallEventAction(int _index)
    {
        OnCallEvent.SetPersistentListenerState(_index, UnityEventCallState.RuntimeOnly);
    }
}
