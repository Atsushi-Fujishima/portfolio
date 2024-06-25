using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRotationObject : MonoBehaviour
{
    public bool isEnable = false;
    [Space]
    [SerializeField] Transform playerTransfrom;
    [Header("Setting Value")]
    [SerializeField] float distance = 3.0f;

    private float defaultHeight = 0;

    private void Start()
    {
        defaultHeight = transform.position.y;
    }

    private void LateUpdate()
    {
        if (isEnable) Rotation();
    }

    private void Rotation()
    {
        var diffPosition= playerTransfrom.forward * distance;
        var playerPosition = playerTransfrom.position;
        var newPosition = diffPosition + playerPosition;

        transform.position = new Vector3(newPosition.x, defaultHeight, newPosition.z);

        transform.forward = playerTransfrom.forward;
    }
}