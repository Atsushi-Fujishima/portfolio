using System.Collections;
using UnityEngine;

public class DeathEventNoramlType : MonoBehaviour
{
    [SerializeField] DeathEventController deathController;
    [Space]
    [SerializeField] GameObject eventSister;
    [SerializeField] SkinnedMeshRenderer sisterMeshRenderer;
    [SerializeField] AudioSource mAudio;
    [SerializeField] AudioClip mAudioClip;
    [Header("Setting")]
    public float eventStartDelay = 3.0f;
    public float blendShapeChangeSpeed = 10.0f;
    public float waitTime = 1.0f;
    public Vector3 setPositionOffset = Vector3.zero;

    public void CallNormalSiterDeathEventStart()
    {
        StartCoroutine(DeathEvent());
    }

    private void EventInitializedBlinkEye()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(EventInitialized, 0, true));
    }

    private void EventInitialized()
    {
        sisterMeshRenderer.SetBlendShapeWeight(2, 0);
        eventSister.SetActive(false);
        deathController.EndDeathEvent();
    }

    private IEnumerator DeathEvent()
    {
        eventSister.SetActive(true);

        yield return new WaitForSeconds(eventStartDelay);

        while (sisterMeshRenderer.GetBlendShapeWeight(2) < 99.0f)
        {
            var weight = sisterMeshRenderer.GetBlendShapeWeight(2);
            weight += blendShapeChangeSpeed * Time.deltaTime;
            sisterMeshRenderer.SetBlendShapeWeight(2, weight); // change blend shape
            yield return null;
        }

        sisterMeshRenderer.SetBlendShapeWeight(2, 100);
        mAudio.PlayOneShot(mAudioClip);
        yield return new WaitForSeconds(waitTime);
        EventInitializedBlinkEye();

        yield break;
    }
}
