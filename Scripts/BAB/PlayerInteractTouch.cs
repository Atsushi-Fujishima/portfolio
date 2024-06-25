using Prototype5;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractTouch : MonoBehaviour
{
    [Header("This system is attached to both hands.")]
    [Space]
    public bool isPermitSystem = true;
    [SerializeField] Rigidbody playerBody;
    [SerializeField] HandHapticManager handHapticManager;
    public string[] validGameObjectTag = { "Enemy", "Item" };
    public string floorTag = string.Empty;
    private bool isTouchFloor = false;
    private HandCollisionController_5 handCollisionController;

    private void Start()
    {
        handCollisionController = GetComponent<HandCollisionController_5>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherObj = collision.gameObject;
        if (otherObj.CompareTag(floorTag))
        {
            isTouchFloor = true;
        }

        if (isTouchFloor == false)
        {
            TouchInteraction(otherObj);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherObj = other.gameObject;
        if (otherObj.CompareTag(floorTag))
        {
            isTouchFloor = true;
        }

        if (isTouchFloor == false)
        {
            TouchInteraction(otherObj);
        }
    }

    private void TouchInteraction(GameObject target)
    {
        if (IsValidObjectTouch(target.tag))
        {
            if (handCollisionController.isLeft)
            {
                handHapticManager.LeftHandHaptic(1.0f, 0.5f);
            }
            else
            {
                handHapticManager.RightHandHaptic(1.0f, 0.5f);
            }

            StartCoroutine(HitLateStopBody());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject otherObj = collision.gameObject;
        if (otherObj.CompareTag(floorTag))
        {
            isTouchFloor = false;
        }
    }

    private bool IsValidObjectTouch(string otherTag)
    {
        foreach (string tag in validGameObjectTag)
        {
            if (otherTag == tag)
            {
                return true;
            }
        }

        return false;
    }

    public void InteractTouchControl(bool set)
    {
        isPermitSystem = set;
    }

    private IEnumerator HitLateStopBody()
    {
        yield return new WaitForSeconds(0.1f);
        playerBody.velocity = Vector3.zero;
        yield break;
    }

    public bool GetTouchFloor()
    {
        return isTouchFloor;
    }
}
