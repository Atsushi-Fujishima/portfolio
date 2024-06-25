using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TutorialController : MonoBehaviour
{
    [SerializeField] TutorialManager tutorialManager;
    [Space]
    [Header("Contollers")]
    [SerializeField] TutorialRunObject runObject;
    [SerializeField] TutorialRotationObject rotationObject;
    [Header("Target")]
    [SerializeField] PlayerTriggerEventController playerTriggerEventController;
    public bool isDisableAction = false;
    public int disableActionIndex = 1;
    [SerializeField] GameObject target;
    [Header("Video")] 
    [SerializeField] VideoPlayer[] videoPlayers;
    [SerializeField] Animator[] videoPanelAnimators;
    [SerializeField] MeshRenderer[] videoRendererMeshes;
    [SerializeField] Material hideVideoMaterial;
    [SerializeField] string[] animatorStateNames = { "Enable", "Disable" };
    [Header("Sound")]
    [SerializeField] AudioSource tutorialControllerAudio;
    [SerializeField] AudioClip activeSoundClip;
    [SerializeField] AudioClip completionSoundClip;
    [SerializeField, Range(0f, 1.0f)] float completionVolume = 1.0f;
    [SerializeField, Range(0f, 1.0f)] float popUIVolume = 0.5f;

    private void OnEnable()
    {
        ActivateTutorial();
    }

    private void ActivateTutorial()
    {
        // video move
        if (runObject != null) runObject.isEnable = true;
        // video rotate
        if (rotationObject != null) rotationObject.isEnable = true;
        // enable animation
        VideoPanelAnimatorControl(animatorStateNames[0]);
        // play videos
        VideoPlay(true);
        // play sound
        PlaySoundEffect(activeSoundClip);
        // enable target
        target.SetActive(true);
        if (playerTriggerEventController != null) playerTriggerEventController.isEnable = true;
    }

    public void CompletionTutorial()
    {
        if (runObject != null) runObject.isEnable = false;
        if (rotationObject != null) rotationObject.isEnable = false;
        VideoPlay(false);
        VideoMaterialHideColor();
        VideoPanelAnimatorControl(animatorStateNames[1]);
        PlaySoundEffect(completionSoundClip);
        target.SetActive(false);
        
        StartCoroutine(EndTutorial());
    }

    private void PlaySoundEffect(AudioClip clip)
    {
        if (clip == completionSoundClip)
        {
            tutorialControllerAudio.volume = completionVolume;
        }
        else
        {
            tutorialControllerAudio.volume = popUIVolume;
        }

        tutorialControllerAudio.PlayOneShot(clip);
    }

    private void VideoPanelAnimatorControl(string set)
    {
        foreach (Animator anim in videoPanelAnimators)
        {
            anim.Play(set);
        }
    }

    private void VideoPlay(bool isPlay)
    {
        if (isPlay)
        {
            foreach (VideoPlayer video in videoPlayers)
            {
                if (video.clip != null)
                {
                    video.Play();
                }
                else
                {
                    MyStaticMethod.DisplayColorLog("y", "Tutorial Controller", "ActivateTutorial", "Not video clip");
                }
            }
        }
        else
        {
            foreach (VideoPlayer video in videoPlayers)
            {
                if (video.clip != null)
                {
                    video.Stop();
                }
                else
                {
                    MyStaticMethod.DisplayColorLog("y", "Tutorial Controller", "ActivateTutorial", "Not video clip");
                }
            }
        }
    }

    private void VideoMaterialHideColor()
    {
        foreach (MeshRenderer mr in videoRendererMeshes)
        {
            mr.material = hideVideoMaterial;
        }
    }

    private IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(1);
        
        if (isDisableAction)
        {
            playerTriggerEventController.DisableCallEventAction(disableActionIndex);
        }

        tutorialManager.NextTutorial();
        gameObject.SetActive(false);
        yield break;
    }
}
