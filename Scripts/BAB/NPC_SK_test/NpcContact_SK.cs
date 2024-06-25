using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NpcContact_SK : MonoBehaviour
{
    [SerializeField] PlayerTalkTransitioner playerTalkTransitioner;
    [SerializeField] Animator animator;
    [SerializeField] NpcContact_Second_SK secondContact;
    public string[] animatorParams = { "Idle", "Eat", "TakeInteract", "Grab" };
    public Transform setPlayerCameraPoint;
    public GameObject targetLight;
    public GameObject addLight;
    private Transform playerTransform;
    [Header("Talk")]
    public Vector3 camRotateOffset = Vector3.one;
    [SerializeField] GameObject speechBubble;
    [SerializeField] TextMeshProUGUI talkTextUGUI;
    [SerializeField] float displayDelay = 0.2f;
    [SerializeField] string[] talkTexts;
    private string setWords = "";
    private string[] wordArr;
    private string words;
    private bool isPermitTalk = true;

    private void Start()
    {
        playerTransform = playerTalkTransitioner.gameObject.transform;
    }

    public void OnFirstContact()
    {
        if (isPermitTalk == false) return;
        StartCoroutine(FirstContact());
    }

    private IEnumerator FirstContact()
    {
        isPermitTalk = false;
        //anim
        animator.SetBool(animatorParams[2], true);
        animator.SetBool(animatorParams[1], false);
        addLight.SetActive(true);
        //player character control
        playerTalkTransitioner.StopPlayerControl();
        
        yield return new WaitForSeconds(0.5f);

        //mabataki
        playerTalkTransitioner.OnPlayerCameraFadeOut(0.5f);

        yield return new WaitForSeconds(1.0f);

        //kaitenn
        transform.LookAt(new Vector3(
            playerTransform.position.x,
            transform.position.y,
            playerTransform.position.z));

        playerTalkTransitioner.OnPlayerCamRotate(camRotateOffset);


        yield return new WaitForSeconds(0.5f);

        //mabataki
        playerTalkTransitioner.OnPlayerCameraFadeIn(0.2f);
        //grab
        animator.SetBool(animatorParams[3], true);
        animator.SetBool(animatorParams[2], false);
        //ueniugoku
 
        yield return new WaitForSeconds(1.0f);
        playerTalkTransitioner.OnPlayerCameraFadeOut(0.5f);
        playerTalkTransitioner.ChangeCameraControl(setPlayerCameraPoint.position);

        yield return new WaitForSeconds(1.0f);
        playerTalkTransitioner.OnPlayerCameraFadeIn(0.2f);

        yield return new WaitForSeconds(3.0f);
        SetTalkText(talkTexts[0]);
        OnDisplayTalkText();


        yield break;
    }

    public void SetTalkText(string _talk)
    {
        if (speechBubble.activeSelf == false) speechBubble.SetActive(true);
        setWords = _talk;
    }

    public void OnDisplayTalkText()
    {
        words = setWords + ",\n";
        wordArr = words.Split(",");
        StartCoroutine(DisplayText());
    }

    private IEnumerator DisplayText()
    {
        foreach (var word in wordArr)
        {
            talkTextUGUI.text += word;
            yield return new WaitForSeconds(displayDelay);
        }

        yield break;
    }

    private IEnumerator EndTalk()
    {
        playerTalkTransitioner.OnPlayerCameraFadeOut(0.5f);

        yield return new WaitForSeconds(1);

        playerTalkTransitioner.ChangeCameraControl(Vector3.zero);
        playerTalkTransitioner.OnPlayerCamRotationInit();
        secondContact.EnableItems();

        yield break;
    }
}
