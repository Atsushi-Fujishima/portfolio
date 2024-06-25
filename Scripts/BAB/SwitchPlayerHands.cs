using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayerHands : MonoBehaviour
{
    [SerializeField] GameObject[] handController;
    [SerializeField] GameObject physicsXRRig;
    private GameObject[] virtualHands;
    private bool isInit = false;
    public enum EnableHandType
    {
        Virtual,
        Physics
    }
    public EnableHandType enableHandType = EnableHandType.Physics;

    private void LateUpdate()
    {
        Initialization();
    }

    public void CallSwitchHand(int _setNum)
    {
        if (_setNum == 0)
        {
            SwitchHands(EnableHandType.Virtual);
        }
        else
        {
            SwitchHands(EnableHandType.Physics);
        }
    }

    public void SwitchHands(EnableHandType enableHandType)
    {
        if (enableHandType == EnableHandType.Virtual)
        {
            physicsXRRig.SetActive(false);

            foreach (var hand in virtualHands)
            {
                hand.SetActive(true);
            }
        }
        else
        {
            physicsXRRig.SetActive(true);

            foreach (var hand in virtualHands)
            {
                hand.SetActive(false);
            }
        }
    }

    private void Initialization()
    {
        if (isInit == false)
        {
            int existence = 0;

            foreach (var controller in handController)
            {
                if (controller.transform.GetChild(0).transform.GetChild(0) != null)
                {
                    existence++;
                }
            }

            if (existence == 2)
            {
                isInit = true;
                virtualHands = new GameObject[] { 
                    handController[0].transform.GetChild(0).transform.GetChild(0).gameObject,
                    handController[1].transform.GetChild(0).transform.GetChild(0).gameObject
                };

                SwitchHands(enableHandType);
            }
        }
    }
}
