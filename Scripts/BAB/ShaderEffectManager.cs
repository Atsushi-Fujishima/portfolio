using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderEffectManager : MonoBehaviour
{
    [SerializeField] string[] shaderCodeNames; // 使用するプロパティ名
    private MeshRenderer m_Renderer;
    private Material m_Material;

    private void Awake()
    {
        m_Renderer = GetComponent<MeshRenderer>(); // コンポーネント参照
        m_Material = m_Renderer.material; // 変数に格納
        m_Renderer.enabled = false; // エフェクトのshaderが優先順位的に邪魔になる可能性があるため、必要になったときにアクティブにする
    }

    // 指定したプロパティのfloat値を取得
    public float GetFloatValue(int _codeIndex)
    {
        return m_Material.GetFloat(shaderCodeNames[_codeIndex]);
    }

    // 指定したプロパティのbool値を取得
    public bool GetBoolValue(int _codeIndex)
    {
        float codeValue = m_Material.GetFloat(shaderCodeNames[_codeIndex]);
        if (codeValue != 0f)
            return true;
        else
            return false;
    }

    // 指定したプロパティのfloat値に値を代入
    public void SetFloatValue(int _codeIndex, float _value)
    {
        m_Material.SetFloat(shaderCodeNames[_codeIndex], _value);
    }

    // 指定したプロパティのbool値に値を代入
    public void SetBoolValue(int _codeIndex, bool _set)
    {
        if (_set)
            m_Material.SetFloat(shaderCodeNames[_codeIndex], 1);
        else
            m_Material.SetFloat(shaderCodeNames[_codeIndex], 0);
    }

    // エフェクトを表示
    public void ShaderEffectEnable()
    {
        m_Renderer.enabled = true;
    }

    // エフェクトを非表示
    public void ShaderEffectDisable()
    {
        m_Renderer.enabled = false;
    }
}
