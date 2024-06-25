using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopRoomConditionControl : MonoBehaviour
{
    public GameObject root0FlagObject;
    public GameObject root1AFlagObject;
    public GameObject root1BFlagObject;
    public GameObject root2AFlagObject;
    public GameObject root2BFlagObject;
    [Space]
    public bool isRoot0Flag = false;
    public bool isRoot1AFlag = false;
    public bool isRoot1BFlag = false;
    public bool isRoot2AFlag = false;
    public bool isRoot2BFlag = false;
    [Space]
    public bool isGetBone = false;
    public bool isGetCube = false;

    public void OriginTrue()
    {
        root0FlagObject.SetActive(true);
        isRoot0Flag = true;
    }

    public void OneATrue()
    {
        root1AFlagObject.SetActive(true);
        isRoot1AFlag = true;
    }

    public void OneBTrue()
    {
        root1BFlagObject.SetActive(true);
        isRoot1BFlag = true;
    }

    public void TwoATrue()
    {
        root2AFlagObject.SetActive(true);
        isRoot2AFlag = true;
    }

    public void TwoBTrue()
    {
        root2BFlagObject.SetActive(true);
        isRoot2BFlag = true;
    }

    public void GetBone()
    {
        isGetBone = true;
    }

    public void GetCube()
    {
        isGetCube = true;
    }
}
