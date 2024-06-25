using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB_SisterSensingController : MonoBehaviour
{
    [SerializeField] LB_SisterController sisterController;
    [SerializeField] PlayerHideController playerHideController;
    [SerializeField] SisterVoiceController sisterVoiceController;

    private void Update()
    {
        if (sisterController.isMove)
        {
            UpdateSensingControl();
        }
    }

    private void UpdateSensingControl()
    {
        if (sisterController.GetMoveState() == sisterController.moveStates[1]) // sister is chased
        {
            if (playerHideController.isHide) // player is hidden
            {
                // change status to hide point break
                sisterController.SetHidePoint(playerHideController.GetHidePoint()); // set controller target hide point 
                sisterController.SetMoveState(sisterController.moveStates[2]); // move hide point
            }
            else
            {
                return;
            }
        }
        else if (sisterController.GetMoveState() == sisterController.moveStates[2]) // sister is break hide point
        {
            if (playerHideController.isHide) // player is hidden
            {
                return;
            }
            else
            {
                // when isMove is true, follows the player if the current state is "HidePoint" and the player is not hidden
                sisterController.SetMoveState(sisterController.moveStates[1]); // is chase
                sisterVoiceController.PlaySoundFind();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerHideController.isHide)
        {
            return;
        }
        else
        {
            if (other.gameObject.tag == "Player")
            {
                sisterController.SetMoveState(sisterController.moveStates[1]); // is chase
                sisterVoiceController.PlaySoundFind();
                MyStaticMethod.DisplayColorLog("b", "SisterSensingController", "is find.", "");
            }
        } 
    }

    private void OnTriggerStay(Collider other)
    {
        if (sisterController.isMove && playerHideController.isHide == false)
        {
            if (other.gameObject.tag == "Player")
            {
                if (sisterController.GetMoveState() != sisterController.moveStates[1])
                {
                    sisterController.SetMoveState(sisterController.moveStates[1]); // is chase
                    sisterVoiceController.PlaySoundFind();
                }
            }
        }
    }

    private void OldCode()
    {
        if (playerHideController.isHide)
        {
            if (sisterController.GetMoveState() == sisterController.moveStates[1]) // is chase
            {
                // HidePointを壊しに行く
                sisterController.SetHidePoint(playerHideController.GetHidePoint()); // set controller target hide point 
                sisterController.SetMoveState(sisterController.moveStates[2]); // move hide point
            }
            else
            {
                return;
            }
        }
        else
        {
            if (sisterController.isMove)
            {
                if (sisterController.GetMoveState() == sisterController.moveStates[2]) // is Hide point destroy
                {
                    // 動いているときに、HPを壊そうとしている場合で、プレイヤーが隠れていないならプレイヤーを追う
                    sisterController.SetMoveState(sisterController.moveStates[1]); // is chase
                    sisterVoiceController.PlaySoundFind();
                }
            }
            else
            {
                return;
            }
        }
    }
}
