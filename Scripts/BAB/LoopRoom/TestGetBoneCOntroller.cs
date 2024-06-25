using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGetBoneCOntroller : MonoBehaviour
{
    public GameObject bone;

    public void GetBoneInteraction()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(GetBone, 0, true));
    }

    private void GetBone()
    {
        bone.SetActive(false);
    }
}
