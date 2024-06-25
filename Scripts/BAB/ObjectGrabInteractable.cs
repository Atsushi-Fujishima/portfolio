using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabInteractable : MonoBehaviour
{
    public Vector3 setLocalPosition = Vector3.zero;
    public Vector3 setLocalRotation = Vector3.zero;
    private Rigidbody rb;
    private Transform thisTransform;
    private GameObject thisGameObject;
    //private int defaultLayer= 0;
    private int grabLayer = 14;
    private bool isRelease = false;
    private bool isGrabed = false;
    private string setHand = string.Empty;
    private PlayerGrabController grabController = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        thisTransform = transform;
        thisGameObject = gameObject;
    }

    private void LateUpdate()
    {
        if (isGrabed == false)
        {
            if (isRelease) Release();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (grabController != null)
        {
            if (collision.gameObject.GetComponent<GameObjectThrowController>() != null)
            {
                Repel(collision.gameObject);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == grabLayer)
        {
            if (isRelease) Release();
        }
    }

    public void OnGrab(Transform parent, string _setHand, PlayerGrabController _grabController)
    {
        //ChangeLayer(grabLayer);
        isGrabed = true;
        setHand = _setHand;
        grabController = _grabController;
        //rb.isKinematic = true;
       // ConstraintsFreezeControl(true);
        //thisTransform.parent = parent;
        //thisTransform.localPosition = Vector3.zero;
        

        //thisTransform.localPosition = setLocalPosition;
        //thisTransform.localEulerAngles = setLocalRotation;
    }

    public void OnDrop()
    {
        //ConstraintsFreezeControl(false);
        //rb.isKinematic = false;
        //thisTransform.parent = null;
        isGrabed = false;
        isRelease = true;
        grabController = null;
    }

    private void ConstraintsFreezeControl(bool _isFreeze)
    {
        if (_isFreeze)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    private void ChangeLayer(int _layer)
    {
        thisGameObject.layer = _layer;
    }

    private void Release()
    {
        isRelease = false;
        //ChangeLayer(defaultLayer);
        setHand = string.Empty;
    }

    private void Repel(GameObject other)
    {
        Vector3 handDirection;
        if (setHand == "Left")
        {
            handDirection = HandDeviceValues.instance.LeftHandDirectionMovement();
        }
        else
        {
            handDirection = HandDeviceValues.instance.RightHandDirectionMovement();
        }

        GameObjectThrowController controller = other.GetComponent<GameObjectThrowController>();
        controller.TakeRepel(handDirection);
    }

    public string CurrentHand()
    {
        return setHand;
    }
}
