using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPunchTutorialControl : MonoBehaviour
{
    public PlayerPunchEventController targetPunchEvent;
    public GameObject punchSilhouette;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractPunchTutorial()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(TutorialEnable, 0, true));
    }

    private void TutorialEnable()
    {
        targetPunchEvent.isEnable = true;
        punchSilhouette.SetActive(true);
    }
}
