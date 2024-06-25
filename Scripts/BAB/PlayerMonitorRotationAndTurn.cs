using UnityEngine;

public class PlayerMonitorRotationAndTurn : MonoBehaviour
{
    [SerializeField] Transform playerCameraTransform;
    [SerializeField] Transform targetTransform;
    [Header("TurnsValue")]
    [SerializeField] float turnsAngle = 50.0f;

    private bool isTurn = false;

    private void Update()
    {
        MonitorTurns();
    }

    private void MonitorTurns()
    {
        Vector3 camPos = playerCameraTransform.position;
        Vector3 targetPos = targetTransform.position;

        Vector3 directionToTarget = targetPos - camPos;
        Vector3 cameraForward = playerCameraTransform.forward;

        // Calculate direction vector and camera orientation angle
        float angle = Vector3.Angle(directionToTarget, cameraForward);

        if (angle < turnsAngle)
        {
            isTurn = true;
        }
        else
        {
            isTurn = false;
        }
    }

    public bool GetIsTurn()
    {
        return isTurn;
    }
}
