using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB_SisterDestroyHidePoint : MonoBehaviour
{
    [SerializeField] SisterVoiceController voiceController;
    [SerializeField] HidePointController[] hidePointControllers;
    [SerializeField] SkinnedMeshRenderer mSkinnedMeshRenderer;
    [SerializeField] GameObject sister;
    [SerializeField] LB_SisterController sisterController;
    [Header("Setting Value")]
    [SerializeField] float delay = 10.0f;
    [SerializeField] float[] destroyEffectDelays = new float[3];
    [Header("Animation")]
    [SerializeField] Animator fuwaAnimator;
    [SerializeField] Animator characterAnimator;
    public float attackAnimationLength = 0f;
    private float elapsedDelay = 0f;
    private int selectedNums = 0;
    private Material mMaterial;
    private bool isDestroy = false;

    private void Start()
    {
        mMaterial = mSkinnedMeshRenderer.sharedMaterial;
        destroyEffectDelays[1] = attackAnimationLength;
    }

    private void Update()
    {
        if (sister.activeSelf)
        {
            //DestroyHidePointControl();
        }
    }

    public void PermitDestroy()
    {
        isDestroy = true;
    }

    private void DestroyHidePointControl()
    {
        if (isDestroy)
        {
            elapsedDelay += Time.deltaTime;
            if (elapsedDelay > delay)
            {
                elapsedDelay = 0f;
                isDestroy = false;
                CallDestroyPoint();
            }
        }
    }

    public void CallDestroyPoint()
    {
        if (selectedNums < hidePointControllers.Length)
        {
            if (sister.activeSelf) StartCoroutine(DestroyHidePointevent());
        }
        else
        {
            return;
        }
    }

    private IEnumerator DestroyHidePointevent()
    {
        yield return new WaitForSeconds(delay);

        if (sister.activeSelf == false) yield break;

        mMaterial.SetFloat("_ColorShift_Speed", 1.0f);
        voiceController.PlaySoundAnger();

        yield return new WaitForSeconds(1.5f);

        if (sister.activeSelf == false) yield break;

        mMaterial.SetFloat("_ColorShift_Speed", 0.0f);
        hidePointControllers[selectedNums].DestroyHidePoint();
        selectedNums++;
        yield break;
    }

    public void Initialization()
    {
        if (mMaterial.GetFloat("_ColorShift_Speed") == 1.0f)
        {
            mMaterial.SetFloat("_ColorShift_Speed", 0.0f);
        }

        selectedNums = 0;
        elapsedDelay = 0f;
        isDestroy = false;
    }

    public void DestroySelectedHidePoint(HidePointController _hp)
    {
        StartCoroutine(DestroyHidePointCoroutine(_hp));
        MyStaticMethod.DisplayColorLog("r", "Destroy Selected Hide Point", "", "");
    }

    private IEnumerator DestroyHidePointCoroutine(HidePointController _hp)
    {
        mMaterial.SetFloat("_ColorShift_Speed", 1.0f);
        fuwaAnimator.SetBool("Idle", true);
        voiceController.PlaySoundAnger();
        yield return new WaitForSeconds(destroyEffectDelays[0]);
        voiceController.PlaySoundAnger();
        characterAnimator.SetBool("Attack", true);
        yield return new WaitForSeconds(destroyEffectDelays[1]);
        characterAnimator.SetBool("Attack", false);
        _hp.DestroyHidePoint();
        yield return new WaitForSeconds(destroyEffectDelays[2]);
        mMaterial.SetFloat("_ColorShift_Speed", 0.0f);
        sisterController.ReStartMove();
        fuwaAnimator.SetBool("Idle", false);
        yield break;
    }
}
