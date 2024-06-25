using System.Collections;
using UnityEngine;

public class PlayerSitChair : MonoBehaviour
{
    [SerializeField] PlayerTalkTransitioner playerTalkTransitioner;
    public Vector3 setPlayerCamRotation = Vector3.zero;
    public Transform setPlayerCamPoint;
    public BoxCollider thisCollider;

    public void OnSit()
    {
        StartCoroutine(PlayerSit());
    }

    private IEnumerator PlayerSit()
    {
        thisCollider.enabled = false;
        playerTalkTransitioner.StopPlayerControl();
        playerTalkTransitioner.OnPlayerCameraFadeOut(0.5f);
        
        yield return new WaitForSeconds(2f);

        playerTalkTransitioner.OnPlayerCamRotate(setPlayerCamRotation);
        playerTalkTransitioner.ChangeCameraControl(setPlayerCamPoint.position);

        yield return new WaitForSeconds(1f);

        playerTalkTransitioner.OnPlayerCameraFadeIn(0.2f);

        yield break;
    }
}
