using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventItemTips : MonoBehaviour
{
    [SerializeField] LanguageManager languageManager;
    [SerializeField] CultTalkManager cultTalkManager;
    public string jp_TipsText = string.Empty;
    public string en_TipText = string.Empty;
    [SerializeField] private string tipsText = string.Empty;

    public void SetTipsText()
    {
        if (languageManager.IsEnglish())
        {
            tipsText = en_TipText;
        }
        else
        {
            tipsText = jp_TipsText;
        }
    }

    public string GetTipsText()
    {
        return tipsText;
    }

    public void DeleteTips()
    {
        cultTalkManager.RemoveEventItemTips(GetTipsText());
    }
}
