using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DollRotationAdjustment : MonoBehaviour
{
    public Transform door;
    public Transform doll;

    private void OnEnable()
    {
        doll = transform;
        RA();
    }

    private void RA()
    {
        doll.transform.forward = door.transform.right;
    }
}
