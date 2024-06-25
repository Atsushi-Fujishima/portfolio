using UnityEngine;

public class FrameRateController : MonoBehaviour
{
    public int targetRate = 60;

    private void Start()
    {
        if (targetRate == 0) return;
        Application.targetFrameRate = targetRate;
        QualitySettings.vSyncCount = 0;
    }
}
