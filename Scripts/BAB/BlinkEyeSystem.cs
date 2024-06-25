using Prototype5;
using System;
using System.Collections;
using UnityEngine;

public class BlinkEyeSystem : MonoBehaviour
{
    [SerializeField] ShaderEffectManager shaderEffectManager; // �V�F�[�_�[�̊Ǘ�
    public float delay = 1.0f; // �����Ȃ�����
    public float fadeTransValue = 1.0f; // ���߂̑��x
    [SerializeField] private float closeEyeThreshold = 0.99f; // �ڂ�����Ɣ��肷��l
    [SerializeField] private float openEyeThreshold = 0.01f; // �ڂ��J�����Ɣ��肷��l
    [Header("Player Control")]
    [SerializeField] HandCollisionController_5[] handCollisionControllers; // ����̏Փˌ��m�𐧌�
    private Action onMethodCallback; // �R�[���o�b�N���\�b�h
    static public BlinkEyeSystem instance; // �C���X�^���X
    private bool isCloseEye = false; // �ڂ������Ԃ������t���O
    private bool isOpenEye = false; // �ڂ��J������Ԃ������t���O

    private void Awake()
    {
        // �C���X�^���X
        if (instance == null)
            instance = this;
    }

    public IEnumerator BlinkEye(Action _callback, float _delay, bool _setHandDetection)
    {
        // ������0�ȉ��ł���Ȃ�ϐ����g�p����B������0���傫���Ȃ�������g�p����B
        var setDelay = (_delay <= 0) ? delay : _delay;

        // ����̌��m���~����
        PlayerHandDetection(false);

        // �ϐ��̏�����
        isCloseEye = false;
        isOpenEye = false;

        // �f���Q�[�g�Ƀ��\�b�h��o�^����
        SetCallbcack(_callback);

        // �ڂ����
        StartCoroutine(CloseEye());
        // True �ɂȂ�܂őҋ@
        yield return new WaitUntil(() => isCloseEye);

        // �f���Q�[�g�Ƀ��\�b�h���o�^����Ă���ΌĂяo��
        onMethodCallback?.Invoke();

        // �^���ÂȎ���
        yield return new WaitForSeconds(setDelay);

        // �ڂ��J����
        StartCoroutine(OpenEye());
        // True �ɂȂ�܂őҋ@
        yield return new WaitUntil(() => isOpenEye);

        // ����̌��m�������ɂ���Đ��䂷��
        PlayerHandDetection(_setHandDetection);

        // End
        yield break;
    }

    private void SetCallbcack(Action _callback)
    {
        onMethodCallback = _callback;
    }

    private void PlayerHandDetection(bool _permit)
    {
        foreach (var hand in handCollisionControllers)
        {
            hand.isNotDetect = !_permit;
        }
    }

    private IEnumerator CloseEye()
    {
        shaderEffectManager.ShaderEffectEnable();
        float value = shaderEffectManager.GetFloatValue(0);
        while (value < closeEyeThreshold)
        {
            yield return null;
            value = shaderEffectManager.GetFloatValue(0);
            value += fadeTransValue * Time.deltaTime;
            shaderEffectManager.SetFloatValue(0, value);
        }

        MyStaticMethod.DisplayColorLog("c", "Compleate Close Eye Coroutine", "", "");
        shaderEffectManager.SetFloatValue(0, 1.0f);
        isCloseEye = true;
        yield break;
    }

    private IEnumerator OpenEye()
    {
        float value = shaderEffectManager.GetFloatValue(0);
        while (value > openEyeThreshold)
        {
            yield return null;
            value = shaderEffectManager.GetFloatValue(0);
            value -= fadeTransValue * Time.deltaTime;
            shaderEffectManager.SetFloatValue(0, value);
        }

        MyStaticMethod.DisplayColorLog("c", "Compleate Open Eye Coroutine", "", "");
        shaderEffectManager.SetFloatValue(0, 0.0f);
        isOpenEye = true;
        shaderEffectManager.ShaderEffectDisable();
        yield break;
    }

    private void OnDestroy()
    {
        // �f���Q�[�g����o�^���ꂽ���\�b�h���폜
        onMethodCallback = null;
    }
}
