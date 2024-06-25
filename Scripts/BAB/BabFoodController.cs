using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabFoodController : MonoBehaviour
{
    private Transform thisTransform;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private bool isDrop = false;
    private bool isEating = false;
    private readonly int changedLayer = 19;
    private readonly int defaultLayer = 0;

    private void Start()
    {
        thisTransform = transform;
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Get(Transform _parent)
    {
        thisTransform.parent = _parent;
        thisTransform.localPosition = Vector3.zero;
        rb.isKinematic = true;
        boxCollider.isTrigger = true;
        isEating = true;
        gameObject.layer = changedLayer;
    }

    public void Drop()
    {
        thisTransform.parent = null;
        rb.isKinematic = false;
        isDrop = true;
        isEating = false;
        gameObject.layer = defaultLayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (boxCollider.isTrigger == false) return;

        if (isDrop) Initialized(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (boxCollider.isTrigger == false) return;

        if (isDrop) Initialized(other.gameObject);
    }

    private void Initialized(GameObject _other)
    {
        if (_other.tag != "Tableware")
        {
            boxCollider.isTrigger = false;
            isDrop = false;
        }
    }

    public bool IsEating()
    {
        return isEating;
    }
}
