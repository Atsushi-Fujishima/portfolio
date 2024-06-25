using TMPro;
using UnityEngine;

public class InteractionOpenText : MonoBehaviour
{
    [SerializeField] LanguageManager languageManager;
    [SerializeField] Transform playerTransform;
    [SerializeField] TextMeshPro targetText;
    [Header("Setting")]
    [SerializeField] float visualizationDistance = 5.0f;
    private GameObject textGameObject;
    [Header("Set Text")]
    public bool isJp = true;
    [SerializeField] string[] setTexts_jp;
    [SerializeField] string[] setTexts_en;
    private string setText = string.Empty;
    [Header("SetPoint")]
    [SerializeField] Transform setPoint;
    private bool isInteracted = false;

    private void Start()
    {
        textGameObject = targetText.gameObject;
        SetTextControl();
    }

    private void Update()
    {
        if (isInteracted)
        {
            UpdateTextGameObject();
            LookAtPlayer();
        }
    }

    private void SetTextControl()
    {
        isJp = !languageManager.IsEnglish();

        if (isJp)
        {
            for (int i = 0; i < setTexts_jp.Length; i++)
            {
                if (i != setTexts_jp.Length - 1)
                {
                    setText += setTexts_jp[i] + "\r\n";
                }
                else
                {
                    setText += setTexts_jp[i];
                }
            }
        }
        else
        {
            for(int i = 0; i < setTexts_en.Length; i++)
            {
                if (i != setTexts_en.Length - 1)
                {
                    setText += setTexts_en[i] + "\r\n";
                }
                else
                {
                    setText += setTexts_en[i];
                }
            }
        }

        targetText.text = setText;
    }

    public void InteractionObject()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(OpenText, 0, true));
    }

    private void OpenText()
    {
        isInteracted = true;

        playerTransform.position = new Vector3(
            setPoint.position.x,
            playerTransform.position.y,
            setPoint.position.z);
        playerTransform.rotation = setPoint.rotation;
    }

    private void UpdateTextGameObject()
    {
        var offset = transform.position - playerTransform.position;
        var length = offset.sqrMagnitude;
        if (length < visualizationDistance * visualizationDistance)
        {
            TextActiveControl(true);
        }
        else
        {
            TextActiveControl(false);
        }
    }

    private void TextActiveControl(bool _Set) 
    {
        if (_Set)
        {
            if (textGameObject.activeSelf == false) textGameObject.SetActive(true);
        }
        else
        {
            if (textGameObject.activeSelf) textGameObject.SetActive(false);
        }
    }

    private void LookAtPlayer()
    {
        if (textGameObject.activeSelf)
        {
            transform.LookAt(new Vector3(
                playerTransform.position.x,
                transform.position.y,
                playerTransform.position.z));
        }
    }
}
