using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessCahndelierController : MonoBehaviour
{
    public bool isFlashing = false;
    public float[] defaultIntensitys;
    [SerializeField] Light[] targetLights;
    [SerializeField] Animator[] lightAnimators;
    [SerializeField] Transform targetTransform;
    private Transform[] lightTransforms = new Transform[2];
    [Header("Value")]
    [SerializeField] float nearDistance = 5.0f;

    private void Start()
    {
        Initialized();
        lightTransforms[0] = targetLights[0].transform;
        lightTransforms[1] = targetLights[1].transform;
    }

    private void Initialized()
    {
        if (isFlashing == false)
        {
            foreach (var light in lightAnimators)
            {
                light.enabled = false;
            }
        }
        else
        {
            foreach (var light in lightAnimators)
            {
                light.enabled = true;
            }
        }

        targetLights[0].intensity = defaultIntensitys[0];
        targetLights[1].intensity = defaultIntensitys[1];
    }

    private void Update()
    {
        if (isFlashing) LightControl();
    }

    private void LightControl()
    {
        var lightAOffset = lightTransforms[0].position - targetTransform.position;
        var lightBOffset = lightTransforms[1].position - targetTransform.position;
        var lightALength = lightAOffset.sqrMagnitude;
        var lightBLength = lightBOffset.sqrMagnitude;

        if (lightALength < nearDistance * nearDistance)
        {
            if (lightAnimators[0].GetBool("Flashing") == false)
                lightAnimators[0].SetBool("Flashing", true);
        }
        else
        {
            lightAnimators[0].SetBool("Flashing", false);
        }

        if (lightBLength < nearDistance * nearDistance)
        {
            if (lightAnimators[1].GetBool("Flashing") == false)
                lightAnimators[1].SetBool("Flashing", true);
        }
        else
        {
            lightAnimators[1].SetBool("Flashing", false);
        }
    }
}
