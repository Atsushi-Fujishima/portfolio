using UnityEngine;
using System.Globalization;

public class LanguageManager : MonoBehaviour
{
    private bool isEnglish = false;
    private readonly string jpCode = "ja";

    [Header("Dev Mode")]
    public bool isDevEnglishMode = false;

    private void Awake()
    {
        if (isDevEnglishMode)
        {
            DevControl();
        }
        else
        {
            GetPCLanguage();
        }
    }

    private void GetPCLanguage()
    {
        var currentCulture = CultureInfo.CurrentCulture;
        string languageCode = currentCulture.TwoLetterISOLanguageName;

        if (languageCode != jpCode)
        {
            isEnglish = true;
        }
    }

    public bool IsEnglish()
    {
        return isEnglish;
    }

    private void DevControl()
    {
        if (isDevEnglishMode)
        {
            isEnglish = true;
        }
    }
}
