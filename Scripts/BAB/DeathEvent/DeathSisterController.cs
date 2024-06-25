using UnityEngine;

public class DeathSisterController : MonoBehaviour
{
    [SerializeField] Transform cameraOffsetTransform;
    [SerializeField] Transform cameraTransform;
    public Vector3 offset = Vector3.zero;
    private Transform mTransform;

    private void OnEnable()
    {
        
    }

    private void SetThisTransform()
    {
        if (mTransform == null) mTransform = transform;
    }
}
