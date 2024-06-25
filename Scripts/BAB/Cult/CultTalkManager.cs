using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CultTalkManager : MonoBehaviour
{
    [SerializeField] LanguageManager languageManager;
    public TalkTextController[] talkTextControllers;
    [SerializeField] CultAnimationController animationController;
    public int selectIndex = 0;
    [Header("Text UGUI")]
    [SerializeField] TextMeshPro[] displayTalkUGUI;
    [Header("Event Item Tips Control")]
    [SerializeField] TalkTextController tipsTalkController;
    [SerializeField] EventItemTips[] eventItemtipss;
    [Header("Tips Text JP")]
    [SerializeField] string jp_firstTalkText = string.Empty;
    [SerializeField] string jp_lastTalkText = string.Empty;
    [SerializeField] string jp_dontNeedTalkText = string.Empty;
    [Header("Tips Text En")]
    [SerializeField] string en_firstTalkText = string.Empty;
    [SerializeField] string en_lastTalkText = string.Empty;
    [SerializeField] string en_dontNeedTalkText = string.Empty;

    private string firstTalkText;
    private string lastTalkText;
    private string dontNeedTalkText;

    private void Start()
    {
        if (languageManager.IsEnglish())
        {
            firstTalkText = en_firstTalkText;
            lastTalkText = en_lastTalkText;
            dontNeedTalkText = en_dontNeedTalkText;
        }
        else
        {
            firstTalkText = jp_firstTalkText;
            lastTalkText = jp_lastTalkText;
            dontNeedTalkText = jp_dontNeedTalkText;
        }
        

        if (tipsTalkController != null)
        {
            TipsConversion();
        }
    }

    public void DisableTalkUI(int _talkControllersIndex)
    {
        displayTalkUGUI[_talkControllersIndex].gameObject.SetActive(false);
    }

    public void EnableTalkUI(int _talkControllersIndex)
    {
        if (_talkControllersIndex == 0)
        {
            StartCoroutine(AwakeTalkDelayEnable());
        }
        else
        {
            displayTalkUGUI[_talkControllersIndex].gameObject.SetActive(true);
        }        
    }

    private IEnumerator AwakeTalkDelayEnable()
    {
        yield return new WaitForSeconds(2);
        displayTalkUGUI[0].gameObject.SetActive(true);
        yield break;
    }

    public void CallDisplayTalk()
    {
        talkTextControllers[selectIndex].OnDisplayTalk();
        animationController.PlayAnimation(CultAnimationController.AnimType.Talk);
    }

    public void AddEventItemTips(string _EventItemTips)
    {
        tipsTalkController.CommonTalkTextListControl("add", _EventItemTips);
    }

    public void RemoveEventItemTips(string _EventItemTips)
    {
        tipsTalkController.CommonTalkTextListControl("remove", _EventItemTips);

        if (tipsTalkController.GetCommonTalkTextListCount() < 3)
        {
            tipsTalkController.CommonTalkTextListControl("clear", string.Empty);
            tipsTalkController.CommonTalkTextListControl("add", dontNeedTalkText);
        }

        tipsTalkController.TalkTextsConversion();
    }

    private void SetTips()
    {
        tipsTalkController.CommonTalkTextListControl("add", firstTalkText);

        foreach (var tips in eventItemtipss)
        {
            tipsTalkController.CommonTalkTextListControl("add", tips.GetTipsText());
        }

        tipsTalkController.CommonTalkTextListControl("add", lastTalkText);

        tipsTalkController.TalkTextsConversion();
    }

    public void SelectTalkControllersIndex(int _talkControllersIndex)
    {
        selectIndex = _talkControllersIndex;
    }

    private void TipsConversion()
    {
        foreach (var tips in eventItemtipss)
        {
            tips.SetTipsText();
        }

        SetTips();
    }
}
