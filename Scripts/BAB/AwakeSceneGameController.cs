using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AwakeSceneGameController : MonoBehaviour
{
    [SerializeField] PlayerCameraHeightSetting playerCameraHeightSetting;
    public int loadSceneIndex = 1;
    public ShaderEffectManager fadeEffect;
    public float startDelay = 1.0f;
    public float transValue = 0.01f;
    private float progressValue = 0;
    AsyncOperation asyncLoad;
    [Header("Tips Control")]
    [SerializeField] PlayerCameraPositionSetter playerCameraPositionSetter;
    [SerializeField] GameObject tipsOwner;
    [SerializeField] GameObject[] tipss;
    [SerializeField] Animator[] tipsAnimators;
    public float setZValue = -5.0f;
    public float readingDelay = 5.0f;
    public float finishedDelay = 0.8f;

    private void Start()
    {
        fadeEffect.ShaderEffectEnable();
        StartCoroutine(TipsControl());
    }

    private IEnumerator TipsControl()
    {
        yield return new WaitUntil(() => playerCameraHeightSetting.GetCompleatedOffset());
        tipsOwner.transform.position = playerCameraPositionSetter.GetWroldPositionRelativeCamera(setZValue);

        yield return new WaitForSeconds(startDelay);

        for (int i = 0; i < tipss.Length; i++)
        {
            tipss[i].SetActive(true);
            yield return new WaitForSeconds(readingDelay);
            tipsAnimators[i].SetBool("Disable", true);
            yield return new WaitForSeconds(finishedDelay);
        }

        StartCoroutine(LateStart());
        yield break;
    }

    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(startDelay);
        LoadSceneAsyncByIndex();
        yield break;
    }

    private void LoadSceneAsyncByIndex()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync(loadSceneIndex);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.89f)
        {
            yield return null;
        }

        while (progressValue < 1.0f)
        {
            progressValue += transValue * Time.deltaTime;
            fadeEffect.SetFloatValue(0, progressValue);
            yield return null;
        }

        fadeEffect.SetFloatValue(0, 1);
        asyncLoad.allowSceneActivation = true;

        while (asyncLoad.isDone == false)
        {
            yield return null;
        }

        yield break;
    }
}
