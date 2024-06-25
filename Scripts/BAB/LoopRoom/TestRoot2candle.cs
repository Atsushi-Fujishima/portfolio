using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoot2candle : MonoBehaviour
{
    public GameObject setLight;
    public GameObject cult;
    public GameObject bone;

    public void CandleGazeInteraction()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(GazeCandle, 0f, true));
    }

    private void GazeCandle()
    {
        setLight.SetActive(true);
        cult.SetActive(false);
        bone.SetActive(true);
    }
}
