using Prototype5;
using System;
using System.Collections;
using UnityEngine;

public class BlinkEyeSystem : MonoBehaviour
{
    [SerializeField] ShaderEffectManager shaderEffectManager; // シェーダーの管理
    public float delay = 1.0f; // 見えない時間
    public float fadeTransValue = 1.0f; // 透過の速度
    [SerializeField] private float closeEyeThreshold = 0.99f; // 目を閉じたと判定する値
    [SerializeField] private float openEyeThreshold = 0.01f; // 目が開いたと判定する値
    [Header("Player Control")]
    [SerializeField] HandCollisionController_5[] handCollisionControllers; // 両手の衝突検知を制御
    private Action onMethodCallback; // コールバックメソッド
    static public BlinkEyeSystem instance; // インスタンス
    private bool isCloseEye = false; // 目を閉じた状態を示すフラグ
    private bool isOpenEye = false; // 目が開いた状態を示すフラグ

    private void Awake()
    {
        // インスタンス
        if (instance == null)
            instance = this;
    }

    public IEnumerator BlinkEye(Action _callback, float _delay, bool _setHandDetection)
    {
        // 引数が0以下であるなら変数を使用する。引数が0より大きいなら引数を使用する。
        var setDelay = (_delay <= 0) ? delay : _delay;

        // 両手の検知を停止する
        PlayerHandDetection(false);

        // 変数の初期化
        isCloseEye = false;
        isOpenEye = false;

        // デリゲートにメソッドを登録する
        SetCallbcack(_callback);

        // 目を閉じる
        StartCoroutine(CloseEye());
        // True になるまで待機
        yield return new WaitUntil(() => isCloseEye);

        // デリゲートにメソッドが登録されていれば呼び出す
        onMethodCallback?.Invoke();

        // 真っ暗な時間
        yield return new WaitForSeconds(setDelay);

        // 目を開ける
        StartCoroutine(OpenEye());
        // True になるまで待機
        yield return new WaitUntil(() => isOpenEye);

        // 両手の検知を引数によって制御する
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
        // デリゲートから登録されたメソッドを削除
        onMethodCallback = null;
    }
}
