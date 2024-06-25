using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] SceneStartup sceneStartup;
    [SerializeField] PlayerTriggerEventController loopDoorTriggerEvent;
    [Space]
    [SerializeField] GameObject[] tutorials;
    [Space]
    [SerializeField] GameObject loopDoorArrow;
    [Header("Value")]
    [SerializeField] float startDelay = 3.0f;
    private bool isStart = false;
    private int tutorialNum = 0;

    private void Start()
    {
        loopDoorTriggerEvent.isEnable = false;
    }

    private void Update()
    {
        if (isStart == false)
        {
            if (sceneStartup.GetIsGamePlayReady())
            {
                isStart = true;
                StartCoroutine(TutorialStart());
            }
        }
    }

    public void NextTutorial()
    {
        tutorialNum++;

        if (tutorialNum < tutorials.Length)
        {
            EnableTutorial(tutorialNum);
        }
        else
        {
            EndTutorial();
        }
    }

    private void EndTutorial()
    {
        loopDoorTriggerEvent.isEnable = true;
        loopDoorArrow.SetActive(true);
    }

    private IEnumerator TutorialStart()
    {
        yield return new WaitForSeconds(startDelay);
        EnableTutorial(0);
        yield break;
    }

    private void EnableTutorial(int num)
    {
        tutorials[num].SetActive(true);
    }
}
