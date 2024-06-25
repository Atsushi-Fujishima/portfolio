using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKeyCubeController : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] GameObject pedestalInteraction;

    public void CubeInteraction()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(CubeControl, 0f, true));
    }

    private void CubeControl()
    {
        cube.SetActive(false);
        pedestalInteraction.SetActive(true);
    }
}
