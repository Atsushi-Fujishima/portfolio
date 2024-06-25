using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrabController : MonoBehaviour
{
    [SerializeField] InputActionReference inputGrab;
    public enum HandName
    {
        Left,
        Right
    }
    public HandName handName;
    private Transform handTransform;
    private bool isEnableGrab = false;
    private bool isGrabing = false;
    private ObjectGrabInteractable currentInteractable;
    private string hand = string.Empty;

    private void Start()
    {
        handTransform = transform;
        HandSwitch();
    }

    private void Update()
    {
        InputGrab();
        if (isEnableGrab == false) Drop();
    }

    private void OnTriggerStay(Collider other)
    {
        GrabTrigger(other);
    }

    private void InputGrab()
    {
        isEnableGrab = inputGrab.action.IsPressed();
    }

    private void Drop()
    {
        if (isGrabing)
        {
            isGrabing = false;
            currentInteractable.OnDrop();
            currentInteractable = null;
        }
    }

    private void GrabTrigger(Collider _other)
    {
        if (isGrabing) return;

        if (isEnableGrab)
        {
            GameObject otherGameObject = _other.gameObject;
            if (otherGameObject.GetComponent<ObjectGrabInteractable>() != null)
            {
                isGrabing = true;
                currentInteractable = otherGameObject.GetComponent<ObjectGrabInteractable>();
                currentInteractable.OnGrab(handTransform, hand, this);
            }
        }
    }

    private void HandSwitch()
    {
        if (handName == HandName.Left) 
        {
            hand = handName.ToString();
        }
        else 
        {
            hand = handName.ToString();
        }
            
    }
}
