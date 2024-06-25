using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoriaGazeLamp : MonoBehaviour
{
    [SerializeField] Light targetLight;
    [SerializeField] MeshRenderer lampRenderer;
    [SerializeField] Material lightsOffMaterial;
    [SerializeField] Material litMaterial;
    [SerializeField] float initializeIntensity = 0.05f;

    private void Start()
    {
        lampRenderer.material = lightsOffMaterial;
        targetLight.intensity = 0;
    }

    public void Interaction()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(StayLamp, 0, true));
    }

    private void StayLamp()
    {
        lampRenderer.material = litMaterial;
        targetLight.intensity = initializeIntensity;
    }
}
