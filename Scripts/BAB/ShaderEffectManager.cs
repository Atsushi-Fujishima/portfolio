using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderEffectManager : MonoBehaviour
{
    [SerializeField] string[] shaderCodeNames; // �g�p����v���p�e�B��
    private MeshRenderer m_Renderer;
    private Material m_Material;

    private void Awake()
    {
        m_Renderer = GetComponent<MeshRenderer>(); // �R���|�[�l���g�Q��
        m_Material = m_Renderer.material; // �ϐ��Ɋi�[
        m_Renderer.enabled = false; // �G�t�F�N�g��shader���D�揇�ʓI�Ɏז��ɂȂ�\�������邽�߁A�K�v�ɂȂ����Ƃ��ɃA�N�e�B�u�ɂ���
    }

    // �w�肵���v���p�e�B��float�l���擾
    public float GetFloatValue(int _codeIndex)
    {
        return m_Material.GetFloat(shaderCodeNames[_codeIndex]);
    }

    // �w�肵���v���p�e�B��bool�l���擾
    public bool GetBoolValue(int _codeIndex)
    {
        float codeValue = m_Material.GetFloat(shaderCodeNames[_codeIndex]);
        if (codeValue != 0f)
            return true;
        else
            return false;
    }

    // �w�肵���v���p�e�B��float�l�ɒl����
    public void SetFloatValue(int _codeIndex, float _value)
    {
        m_Material.SetFloat(shaderCodeNames[_codeIndex], _value);
    }

    // �w�肵���v���p�e�B��bool�l�ɒl����
    public void SetBoolValue(int _codeIndex, bool _set)
    {
        if (_set)
            m_Material.SetFloat(shaderCodeNames[_codeIndex], 1);
        else
            m_Material.SetFloat(shaderCodeNames[_codeIndex], 0);
    }

    // �G�t�F�N�g��\��
    public void ShaderEffectEnable()
    {
        m_Renderer.enabled = true;
    }

    // �G�t�F�N�g���\��
    public void ShaderEffectDisable()
    {
        m_Renderer.enabled = false;
    }
}
