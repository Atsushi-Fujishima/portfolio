using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialRunObject : MonoBehaviour
{
    public bool isEnable = false;
    [Space]
    [SerializeField] Transform playerTransform;
    [Header("Setting Value")]
    [SerializeField] float nearDistance = 0.5f;
    [SerializeField] Vector3 distanceLimit;

    private Vector3 totalDistanceMoved;
    private float defaultHeight = 0;

    private void Start()
    {
        defaultHeight = transform.position.y;
    }

    private void LateUpdate()
    {
        if (isEnable) Run();
    }

    private void Run()
    {
        var targetPosition = playerTransform.position;
        var newPosition = new Vector3(targetPosition.x, defaultHeight, targetPosition.z + nearDistance);

        float deltaX = newPosition.x - transform.position.x;
        float deltaZ = newPosition.z - transform.position.z;

        if (Mathf.Abs(totalDistanceMoved.x + deltaX) <= distanceLimit.x)
        {
            newPosition.x = targetPosition.x;
            totalDistanceMoved.x += deltaX;
        }
        else
        {
            newPosition.x = transform.position.x;
        }

        if (Mathf.Abs(totalDistanceMoved.z + deltaZ) <= distanceLimit.z)
        {
            newPosition.z = targetPosition.z + nearDistance;
            totalDistanceMoved.z += deltaZ;
        }
        else
        {
            newPosition.z = transform.position.z;
        }

        transform.position = newPosition;
    }
}
