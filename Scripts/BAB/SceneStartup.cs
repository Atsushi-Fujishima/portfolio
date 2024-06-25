using System.Collections;
using UnityEngine;

public class SceneStartup : MonoBehaviour
{
    [SerializeField] PlayerCameraHeightSetting playerCameraHeightSetting;
    [SerializeField] ShaderEffectManager startupFadeManager;
    public float fadeChangeSpeed = 1.0f;
    private bool isGamePlayReady = false;

    private void Start()
    {
        startupFadeManager.SetFloatValue(0, 1f);
        startupFadeManager.ShaderEffectEnable();
    }

    private void Update()
    {
        if (isGamePlayReady == false)
        {
            WaitingForConnection();
        }
    }

    private void WaitingForConnection()
    {
        if (playerCameraHeightSetting.GetCompleatedOffset())
        {
            isGamePlayReady = true;
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        float value = startupFadeManager.GetFloatValue(0);
        while (value > 0.01f)
        {
            value = startupFadeManager.GetFloatValue(0);
            value -= fadeChangeSpeed * Time.deltaTime;
            startupFadeManager.SetFloatValue(0, value);
            yield return null;
        }

        startupFadeManager.SetFloatValue(0, 0f);
        startupFadeManager.ShaderEffectDisable();
        yield break;
    }

    public bool GetIsGamePlayReady()
    {
        return isGamePlayReady;
    }
}
