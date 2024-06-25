using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkTextController2 : MonoBehaviour
{
    [SerializeField] TextMeshPro talkTextUGUI;
    public Transform playerTransform;
    public Transform setPlayerPoint;
    [SerializeField] LoopController loopController;
    [SerializeField] Animator animator;
    [Space]
    public bool isAwake = false;
    public string[] talkTexts;
    [SerializeField] float displayCharacterDelay = 0.2f;
    [SerializeField] float displayTextDelay = 1.0f;
    public List<string> resultTexts = new List<string>();
    private string[] resultTextArry;
    private bool isFinishTalk = false;

    private void Start()
    {
        talkTextUGUI.text = string.Empty;

        for (int i = 0; i < talkTexts.Length; i++)
        {
            string t = talkTexts[i];
            string result = string.Join(",", t.ToCharArray());
            result = t[0] + result.Substring(1, result.Length - 2) + t[t.Length - 1];
            resultTexts.Add(result);
        }

        if (isAwake)
        {
            OnDisplayTalk();
        }
    }

    public void OnDisplayTalk()
    {
        StartCoroutine(DisplayText());
    }

    private IEnumerator DisplayText()
    {
        foreach (string text in resultTexts)
        {
            talkTextUGUI.text = string.Empty;
            resultTextArry = text.Split(",");

            foreach (var character in resultTextArry)
            {
                talkTextUGUI.text += character;
                yield return new WaitForSeconds(displayCharacterDelay);
            }

            yield return new WaitForSeconds(displayTextDelay);
        }

        yield return new WaitForSeconds(displayTextDelay);
        talkTextUGUI.text = string.Empty;
        if (loopController != null) loopController.PlaySign();
        animator.SetBool("Talk", false);
        isFinishTalk = true;
        yield break;
    }

    public void OnTalkInteraction()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(TalkEvent, 0, true));
    }

    public void TalkEvent()
    {
        playerTransform.position = new Vector3(
            setPlayerPoint.position.x,
            playerTransform.position.y,
            setPlayerPoint.position.z);
        playerTransform.rotation = setPlayerPoint.rotation;

        animator.SetBool("Talk", true);
        OnDisplayTalk();
    }

    public bool IsFinishTalk()
    {
        return isFinishTalk;
    }
}
