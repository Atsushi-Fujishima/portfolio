using System.Collections;
using UnityEngine;
using UnityEditor;

public class CreditController : MonoBehaviour
{
    public LastLoopRoomController lastLoopRoomController;
    [Header("Credit")]
    public GameObject creditParent;
    public Transform creditTransform;
    public Vector3 direction = Vector3.up;
    public float scrollSpeed = 1f;
    public float stopScrollAmount = 30.0f;
    private float totalScrollAmount = 0f;
    private GameObject creditObject;
    [Header("Fade")]
    [SerializeField] ShaderEffectManager fadeEffectManager;
    public float fadeSpeed = 1f;
    [Header("Audio")]
    [SerializeField] AudioSource endingAudio;
    public float changeVolumeSpeed = 5f;

    private bool isFadeCompleate = false;
    private bool isSoundStopComleate = false;

    private void Start()
    {
        creditObject = creditTransform.gameObject;
    }

    private void Update()
    {
        

        if (creditParent.activeSelf)
        {
            if (creditObject.activeSelf) 
                CreditScroll();

            if (lastLoopRoomController.GetPlayerNotMove() == false)
                lastLoopRoomController.StopPlayer();
        }    

        ExitGameControl();
    }

    private void CreditScroll()
    {
        totalScrollAmount += direction.y * Time.deltaTime;
        creditTransform.position += direction * scrollSpeed * Time.deltaTime;
        // Debug.Log(totalScrollAmount);

        if (totalScrollAmount > stopScrollAmount)
        {
            creditObject.SetActive(false);
            StartCoroutine(EndFadeControl());
            StartCoroutine(EndSoundControl());
        }
    }

    private IEnumerator EndFadeControl()
    {
        fadeEffectManager.ShaderEffectEnable();

        while (fadeEffectManager.GetFloatValue(0) < 0.99f)
        {
            var value = fadeEffectManager.GetFloatValue(0);
            value += fadeSpeed * Time.deltaTime;
            fadeEffectManager.SetFloatValue(0, value);
            yield return null;
        }

        fadeEffectManager.SetFloatValue(0, 1.0f);
        isFadeCompleate = true;
        yield break;
    }

    private IEnumerator EndSoundControl()
    {
        while (endingAudio.volume > 0.1f)
        {
            var volume = endingAudio.volume;
            volume -= changeVolumeSpeed * Time.deltaTime;
            endingAudio.volume = volume;
            yield return null;
        }

        endingAudio.volume = 0f;
        isSoundStopComleate = true;
        yield break;
    }

    private void ExitGameControl()
    {
        if (isFadeCompleate && isSoundStopComleate)
        {
            isFadeCompleate = false;
            isSoundStopComleate = false;
            StartCoroutine(ExitDelay());
        }
    }

    private IEnumerator ExitDelay()
    {
        yield return new WaitForSeconds(2);
        ExitGame();
    }

    private void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
