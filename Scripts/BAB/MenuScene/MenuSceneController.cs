using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneController : MonoBehaviour
{
    public Camera playerCamera;
    [Header("Touch Guide")]
    [SerializeField] GameObject touchText;
    [Header("Check the front")]
    [SerializeField] GameObject frontText;
    [SerializeField] GameObject settingsGuide;
    [SerializeField] GameObject confirmationButton;
    [Header("Game Start")]
    [SerializeField] GameObject startButton;
    public int callSceneIndex = 1;

    private Transform playerCameraTransform;

    private void Start()
    {
        playerCameraTransform = playerCamera.transform;
    }

    private void Update()
    {
        if (settingsGuide.activeSelf)
        {
            SettingFrontTextPosition();
        }
    }

    public void TouchButtonControl(int menuStepNum)
    {
        if (menuStepNum == 0)
        {
            touchText.SetActive(false);
            frontText.SetActive(true);
            settingsGuide.SetActive(true);
            StartCoroutine(DelayActivateButton(confirmationButton));
        }
        else if (menuStepNum == 1)
        {
            frontText.SetActive(false);
            settingsGuide.SetActive(false);

            StartCoroutine(DelayActivateButton(startButton));
        }
        else
        {
            StartCoroutine(DelayLoadScene());
        }
    }

    private IEnumerator DelayLoadScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(callSceneIndex);
    }

    private IEnumerator DelayActivateButton(GameObject button)
    {
        yield return new WaitForSeconds(2);
        button.SetActive(true);
        yield break;
    }

    private void SettingFrontTextPosition()
    {
        frontText.transform.position = new Vector3(
            frontText.transform.position.x,
            playerCameraTransform.position.y,
            frontText.transform.position.z);
    }
}
