using UnityEngine;

public class PlayerCameraPositionSetter : MonoBehaviour
{
    private Camera mCamera;
    private Transform mCameraTransform;

    private void Awake()
    {
        mCamera = Camera.main;
        mCameraTransform = mCamera.gameObject.transform;
    }

    public Vector3 GetWroldPositionRelativeCamera(float setZAxisValue)
    {
        Vector3 camPos = mCameraTransform.position;
        Vector3 setPos = new Vector3(
            camPos.x,
            camPos.y,
            camPos.z + setZAxisValue
            );

        return setPos;
    }
}
