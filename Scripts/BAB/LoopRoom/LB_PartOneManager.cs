using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB_PartOneManager : MonoBehaviour
{
    [Header("Sister Control")]
    [SerializeField] GameObject sister;
    [SerializeField] LB_SisterController sisterController;
    [SerializeField] float activateCycleTime = 30.0f;
    private float elapsedCycleTime = 0;
    private bool isDormant = false;
    [Header("Light Control")]
    [SerializeField] Animator[] lightAnimators;
    [SerializeField] string[] parameterNames = { "Default", "Flashing" };
    [Header("Destroy Hide Point Control")]
    [SerializeField] LB_SisterDestroyHidePoint destroyHidePoint;
    [Header("Hide Point")]
    [SerializeField] HidePointController[] hidePoints;
    [Header("Start Setting")]
    [SerializeField] GameObject[] spels;
    [SerializeField] AudioSource signAudio;

    private void Start()
    {
        LightControl(parameterNames[0]);
    }

    private void Update()
    {
        if (isDormant) ActivateTimeControl();
    }

    private void ActivateTimeControl()
    {
        elapsedCycleTime += Time.deltaTime;
        if (elapsedCycleTime > activateCycleTime)
        {
            elapsedCycleTime = 0;
            isDormant = false;
            ActivateSister();
        }
    }

    public void ActivateSister()
    {
        sister.SetActive(true);
        destroyHidePoint.PermitDestroy(); // after delay destroy hide point      
        LightControl(parameterNames[1]);
    }

    // part one sister's controller calls
    public void DisableSister()
    {
        sister.SetActive(false);
        InitializeLight(); // init the light
        isDormant = true;
    }

    private void LightControl(string _paramName)
    {
        if (_paramName == parameterNames[0])
        {
            foreach (var l in lightAnimators)
            {
                l.Play(_paramName);
            }
        }
        else
        {
            foreach (var l in lightAnimators)
            {
                l.Play(_paramName);
            }
        }
    }

    private void InitializeLight()
    {
        LightControl(parameterNames[0]);
    }

    public void InitializationPartOneSystem()
    {
        isDormant = false; // sister system
        elapsedCycleTime = 0; // sister system
        InitializeLight(); // sister system
        destroyHidePoint.Initialization(); // sister system
        sisterController.InitializeMove(); // sister system
        if (sister.activeSelf) sister.SetActive(false); // sister system

        foreach (var hp in hidePoints)
        {
            hp.Initialization(); // hide point system
        }
    }

    public void PartOneStart()
    {
        foreach (var spel in spels)
        {
            spel.SetActive(false);
        }

        signAudio.Play();
    }
}
