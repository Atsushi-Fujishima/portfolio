using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootBranchControl : MonoBehaviour
{
    public int currentRootNumber = 1;
    public LoopRoomConditionControl conditionControl;

    public GameObject root0;
    public GameObject root1;
    public GameObject root2;
    public GameObject root3;

    public void RootControl()
    {
        if (currentRootNumber == 0)
        {
            if (conditionControl.isRoot0Flag)
            {
                root0.SetActive(false);
                root1.SetActive(true);
                currentRootNumber = 1;
            }
        }
        else if (currentRootNumber == 1)
        {
            if (conditionControl.isRoot1AFlag && conditionControl.isRoot1BFlag)
            {
                root1.SetActive(false);
                root2.SetActive(true);
                currentRootNumber = 2;
            }
            else
            {
                root1.SetActive(false);
                root0.SetActive(true);
            }
        }
        else if (currentRootNumber == 2)
        {
            if (conditionControl.isRoot2AFlag && conditionControl.isRoot2BFlag)
            {
                root2.SetActive(false);
                root3.SetActive(true);
                currentRootNumber = 3;
            }
            else
            {
                root2.SetActive(false);
                root1.SetActive(true);
                currentRootNumber = 1;
            }
        }
    }
}
