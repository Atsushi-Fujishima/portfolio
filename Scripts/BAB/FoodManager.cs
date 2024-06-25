using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public enum FoodType
    {
        Safety,
        Danger
    }
    public FoodType foodType = FoodType.Safety;

    [Header("Smell Effect")]
    [SerializeField] MeshRenderer smellEffectRenderer;
    private Animator smellAnimator;
    public float triggerDistance = 0.3f;
    [Header("Stabbed Effect")]
    [SerializeField] ParticleSystem stabEffect;
    [Header("Drop Effect")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip dropEffectClip;

    private Transform playerCameraTransform;

    private void Start()
    {
        playerCameraTransform = Camera.main.gameObject.transform;
        smellAnimator = smellEffectRenderer.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (foodType == FoodType.Danger)
        {
            var offset = playerCameraTransform.position - transform.position;
            var sqrlength = offset.sqrMagnitude;
            if (sqrlength < triggerDistance * triggerDistance)
            {
                if (smellEffectRenderer.enabled == false)
                {
                    smellAnimator.Play("Enable");
                }
            }
        }
    }
}
