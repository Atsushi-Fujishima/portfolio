using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetCultEvent : MonoBehaviour
{
    public GameObject cult;
    public CultMoveController cultMoveController;
    

    public void CallMeetCult()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(Meet, 0, true));
    }

    private void Meet()
    {
        StartCoroutine(MeetCor());
    }

    private IEnumerator MeetCor()
    {
        cult.SetActive(true);
        yield return null;
        cultMoveController.PlayMove(CultMoveController.MoveType.Up);
        yield break;
    }
}
