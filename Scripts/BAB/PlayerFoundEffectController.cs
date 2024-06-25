using System.Collections;
using UnityEngine;

public class PlayerFoundEffectController : MonoBehaviour
{
    public enum EffectControlType { Auto, Compulsion, Stop};
    [SerializeField] ShaderVisualEffectManager foundEffectManager;
    [Header("Settings")]
    public EffectControlType controlType = EffectControlType.Auto;
    [SerializeField] float blinkingValueMin = 0.75f;
    [SerializeField] float blinkingValueMax = 0.9f;
    [SerializeField] float initChangeSpeed = 1.0f;
    [SerializeField] float repeatingSpeed = 0.1f;
    [Header("Trigger")]
    [SerializeField] GameObject lbSister;
    [SerializeField] LB_SisterController sisterController;

    private GameObject shaderEffectGameObject = null;
    private float elapsedTime = 0f;

    public bool isDevActivete = false;

    private void Start()
    {
        shaderEffectGameObject = foundEffectManager.gameObject;
    }

    private void Update()
    {
        EffectTypeManagement();
    }

    private void EffectTypeManagement()
    {
        switch (controlType)
        {
            case EffectControlType.Auto: AutoFoundEffectControl(); break;
            case EffectControlType.Compulsion: CompulsionFoundeEffectControl(); break;
            case EffectControlType.Stop: break;
        }
    }

    private void AutoFoundEffectControl()
    {
        if (lbSister.activeSelf && lbSister.transform.parent.gameObject.activeSelf)
        {
            if (sisterController.GetMoveState() == sisterController.moveStates[1])
            {
                if (shaderEffectGameObject.activeSelf == false) EffectActivateControl(true);
                FoundEffectUpdate();
            }
            else
            {
                // auto no mama daga effect ha kieteiku
                DecreaseValue();
            }
        }
        else
        {
            // auto no mama daga effect ha kieteiku
            if (shaderEffectGameObject.activeSelf) DecreaseValue();
        }
    }

    private void CompulsionFoundeEffectControl()
    {
        if (shaderEffectGameObject.activeSelf == false) EffectActivateControl(true);
        FoundEffectUpdate();
    }

    private void FoundEffectUpdate()
    {
        if (foundEffectManager.GetFloatValue(0) < blinkingValueMin)
        {
            if (elapsedTime != 0f) elapsedTime = 0f;
            InitialIncrease();
        }
        else
        {
            RepeatingValue();
        }
    }

    private void InitialIncrease()
    {
        var value = foundEffectManager.GetFloatValue(0);
        value += initChangeSpeed * Time.deltaTime;
        foundEffectManager.SetFloatValue(0, value);
    }

    private void RepeatingValue()
    {
        elapsedTime += repeatingSpeed * Time.deltaTime;
        var value = Mathf.PingPong(elapsedTime, blinkingValueMax -  blinkingValueMin) + blinkingValueMin;
        foundEffectManager.SetFloatValue(0, value);
    }

    private void DecreaseValue()
    {
        var value = foundEffectManager.GetFloatValue(0);

        if (value > 0.1f)
        {
            value -= initChangeSpeed * Time.deltaTime;
            foundEffectManager.SetFloatValue(0, value);
        }
        else
        {
            foundEffectManager.SetFloatValue(0, 0f);
            EffectActivateControl(false);
        }
    }

    private void EffectActivateControl(bool isActive)
    {
        shaderEffectGameObject.SetActive(isActive);
    }

    public IEnumerator FoundEffectOutage()
    {
        controlType = EffectControlType.Stop;

        if (shaderEffectGameObject.activeSelf == false)
        {
            foundEffectManager.SetFloatValue(0, 0f);
            yield break;
        }

        while (foundEffectManager.GetFloatValue(0) > 0.1f)
        {
            var value = foundEffectManager.GetFloatValue(0);
            value -= initChangeSpeed * Time.deltaTime;
            foundEffectManager.SetFloatValue(0, value);
            yield return null;
        }

        foundEffectManager.SetFloatValue(0, 0f);
        EffectActivateControl(false);
        yield break;
    }

    public void SetTypeAuto()
    {
        controlType = EffectControlType.Auto;
    }

    public void SetTypeCompulsion()
    {
        controlType = EffectControlType.Compulsion;
    }

    public void SetTypeStop()
    {
        StartCoroutine(FoundEffectOutage());
    }
}
