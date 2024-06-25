using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TalkTextController : MonoBehaviour
{
    [SerializeField] LanguageManager languageManager;
    public enum TalkType { Static, Dynamic};
    public TalkType talkType = TalkType.Dynamic;
    [SerializeField] TextMeshPro talkText;
    [Header("Setting")]
    public bool isAwake = false;
    public bool isStartConversion = true;
    [SerializeField] float displayCharacterTimeIntercal = 0.1f;
    [SerializeField] float displayTextDelay = 2.5f;
    [SerializeField] float awakeDelay = 0f;
    [Header("Text")]
    public List<string> talkTexts = new List<string>();
    public string[] enTalks = new string[0];
    private List<string> commonTalkTextList = new List<string>();
    [Header("Talk End Events")]
    public UnityEvent OnCallTalkEndEvent;

    private List<string> resultTexts = new List<string>();
    private string[] listControlKeys = { "add", "remove", "clear" };

    private void Awake()
    {
        SetCommonTalkList();

    }

    private void OnEnable()
    {
        TalkTextInitialized();
    }

    private void Start()
    {
        if (talkType == TalkType.Dynamic)
        {
            if (isStartConversion) TalkTextsConversion();
        }

        if (isAwake)
        {
            StartCoroutine(FreestandingAwakeDisplayText());
        }
    }

    private IEnumerator FreestandingAwakeDisplayText()
    {
        yield return new WaitForSeconds(awakeDelay);
        OnDisplayTalk();
        yield break;
    }

    private void SetCommonTalkList()
    {
        if (languageManager.IsEnglish())
        {
            commonTalkTextList.AddRange(enTalks);
        }
        else
        {
            commonTalkTextList.AddRange(talkTexts);
        }
    }

    public void OnDisplayTalk()
    {
        switch (talkType)
        {
            case TalkType.Static: DisplayStaticText(); break;
            case TalkType.Dynamic: StartCoroutine(DisplayDynamicText()); break;
        }
    }

    private void DisplayStaticText()
    {
        talkText.text = string.Empty;

        talkText.text = commonTalkTextList[0];

        OnTalkEndEvent();
    }

    private IEnumerator DisplayDynamicText()
    {
        yield return new WaitForSeconds(2.0f);

        foreach (string text in resultTexts)
        {
            
            talkText.text = string.Empty;
            var resultTextArry = text.Split(",");

            foreach (var character in resultTextArry)
            {
                talkText.text += character;
                yield return new WaitForSeconds(displayCharacterTimeIntercal);
            }

            yield return new WaitForSeconds(displayTextDelay);
        }

        yield return new WaitForSeconds(displayTextDelay);
        
        TalkTextInitialized();
        OnTalkEndEvent();
        yield break;
    }

    public void TalkTextsConversion()
    {
        resultTexts.Clear();

        for (int i = 0; i < commonTalkTextList.Count; i++)
        {
            string t = commonTalkTextList[i];
            string result = string.Join(",", t.ToCharArray());
            result = t[0] + result.Substring(1, result.Length - 2) + t[t.Length - 1];
            resultTexts.Add(result);
        }
    }

    private void OnTalkEndEvent()
    {
        OnCallTalkEndEvent?.Invoke();
    }

    private void TalkTextInitialized()
    {
        talkText.text = string.Empty;
    }

    public void CommonTalkTextListControl(string controlKey, string text)
    {
        if (controlKey == listControlKeys[0])
        {
            // add
            commonTalkTextList.Add(text);
        }
        else if (controlKey == listControlKeys[1])
        {
            // remove
            commonTalkTextList.Remove(text);
        }
        else
        {
            // clear
            commonTalkTextList.Clear();
        }
    }

    public int GetCommonTalkTextListCount()
    {
        return commonTalkTextList.Count;
    }
}
