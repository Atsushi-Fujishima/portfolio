using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGroupController : MonoBehaviour
{
    public Transform parentTranform;
    public Transform meshTransform;
    public Transform leftHandPivot;
    public Transform rightHandPivot;
    [Header("Target")]
    [SerializeField] Transform handLeftMaster;
    [SerializeField] Transform handRightMaster;

    private void LateUpdate()
    {
        if (parentTranform.gameObject.activeSelf)
        {
            Tracking();
        } 
    }

    private void Tracking()
    {
        // left tracking
        leftHandPivot.position = handLeftMaster.position;
        leftHandPivot.rotation = handLeftMaster.rotation;

        // right tracking
        rightHandPivot.position = handRightMaster.position;
        rightHandPivot.rotation = handRightMaster.rotation;
    }

    public void HandsActiveControl(bool value)
    {
        if (value)
            parentTranform.gameObject.SetActive(true);
        else
            parentTranform.gameObject.SetActive(false);
    }
}
