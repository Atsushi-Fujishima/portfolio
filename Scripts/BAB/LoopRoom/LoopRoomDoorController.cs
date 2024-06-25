using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopRoomDoorController : MonoBehaviour
{
    [SerializeField] Animator startDoorAnimator;
    [SerializeField] Animator otherDoorAnimator;
    [SerializeField] AudioSource startDoorAudio;
    [SerializeField] AudioSource otherDoorAudio;
    [SerializeField] AudioClip sfxOpenDoor;

    public void OpenStartDoor()
    {
        startDoorAnimator.SetBool("Open", true);
        startDoorAudio.PlayOneShot(sfxOpenDoor);
    }

    public void OpenOtherDoor()
    {
        otherDoorAnimator.SetBool("Open", true);
        otherDoorAudio.PlayOneShot(sfxOpenDoor);
    }

    public void OpenMultiDoor()
    {
        StartCoroutine(DoorControl());
    }

    private IEnumerator DoorControl()
    {
        OpenOtherDoor();
        OpenStartDoor();

        yield return new WaitForSeconds(2);

        otherDoorAnimator.SetBool("Open", false);

        yield break;
    }
}
