using System.Collections;
using UnityEngine;

public class HorrorEventManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform setPlayerPoint;
    [Header("Sister Look")]
    [SerializeField] GameObject eventSister;
    [SerializeField] HorrorEventSisterController sisterController;
    [Header("FootStep")]
    [SerializeField] HorrorEventFootstepsController footstepsController;
    [SerializeField] float footstepDelay = 1.5f;
    [Header("Sister Attack")]
    [SerializeField] PlayerTriggerEventController sisterAttackTrigger;

    public void HorrorEventStart()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(LookSister, 0, false));
        MyStaticMethod.DisplayColorLog("c", this.name, "Start Horror Event", "");
    }

    private void LookSister()
    {
        SetPlayerPoint(); // look sister
        eventSister.SetActive(true); // activated sister
        sisterController.LookStart(); // start sister head rotation 
        StartCoroutine(SisterLookAtPlayerManagement()); // lookAtPlayer management
    }

    private IEnumerator SisterLookAtPlayerManagement()
    {
        yield return new WaitUntil(() => sisterController.GetEndLook());

        MyStaticMethod.DisplayColorLog("c", this.name, "End Sister Look At Player", "");

        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(EndLookEvent, 0, true));

        yield break;
    }

    private void EndLookEvent()
    {
        sisterController.HiddenSister();
        playerTransform.rotation = Quaternion.identity;
        StartCoroutine(StartFootstepEvent());
        EnableSisterAttackTrigger();
    }

    private void PlayFootStep()
    {
        footstepsController.StartMove();
        
    }

    private void EnableSisterAttackTrigger()
    {
        sisterAttackTrigger.isEnable = true;
    }

    private IEnumerator StartFootstepEvent()
    {
        yield return new WaitForSeconds(footstepDelay);

        PlayFootStep();

        yield break;
    }

    public void StartSisterAttack()
    {
        StartCoroutine(SisterAttackCor());
    }

    private IEnumerator SisterAttackCor()
    {
        sisterController.gameObject.SetActive(true);
        yield return null;
        sisterController.StartAttack();
    }

    private void SetPlayerPoint()
    {
        playerTransform.position = new Vector3(
            setPlayerPoint.position.x,
            playerTransform.position.y,
            setPlayerPoint.position.z);
        playerTransform.rotation = setPlayerPoint.rotation;
    }
}
