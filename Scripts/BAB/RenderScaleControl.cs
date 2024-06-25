using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RenderScaleControl : MonoBehaviour
{
    public UniversalRenderPipelineAsset urpAsset;
    private float recommendationScale = 1.0f;

    private void Start()
    {
        recommendationScale = urpAsset.renderScale; 
    }

    public void ChangeRenderScale(float _Scale)
    {
        urpAsset.renderScale = _Scale;
    }

    public void InitializeRenderScele()
    {
        urpAsset.renderScale = recommendationScale;
    }
}
