using TMPro;
using UnityEngine;

public class TipsTextControl : MonoBehaviour
{
    [SerializeField] LanguageManager languageManager;
    private TextMeshPro tipsText;
    [Space]
    public string[] jaTexts;
    [Space]
    public string[] enTexts;

    private void Start()
    {
        DisplayTextControl();
    }

    private void DisplayTextControl()
    {
        tipsText = tipsText = GetComponent<TextMeshPro>();
        tipsText.text = string.Empty;

        if (languageManager.IsEnglish() == false)
        {
            DisplayText(jaTexts);
        }
        else
        {
            DisplayText(enTexts);
        }
    }

    private void DisplayText(string[] _Texts)
    {
        var texts = _Texts;
        for (int i = 0; i < texts.Length; i++)
        {
            if (i + 1 < texts.Length)
            {
                string getText = texts[i];
                string setText = getText + "\n";
                tipsText.text += setText;
            }
            else
            {
                string getText = texts[i];
                tipsText.text += getText;
            }
        }
    }
}
