using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpelController : MonoBehaviour
{
    [Header("Rotaiton")]
    public float rotationSpeed = 5.0f;
    private bool isEnable = true;
    [Header("L8 Master Control")]
    [SerializeField] DecisiveBattleController decisiveBattleController;
    [Header("Collision Target")]
    [SerializeField] GameObject[] targets;
    [Header("Mesh")]
    [SerializeField] MeshRenderer mRenderer;
    private Material mMaterial;
    public float decAlphaValue = 1.0f;
    [Header("Sound")]
    [SerializeField] AudioSource mAudioSource;
    [SerializeField] AudioClip mAudioClip;

    private void Start()
    {
        mMaterial = mRenderer.material;
    }

    private void Update()
    {
        if (isEnable) 
            Roate();
        else
            ExtinctionSpel();
    }

    private void Roate()
    {
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isEnable)
        {
            foreach (var target in targets)
            {
                if (collision.gameObject.tag == "Player")
                {
                    Extinction();
                    break;
                }
            }
        }
    }

    private void Extinction()
    {
        isEnable = false;
        decisiveBattleController.DestructionOfRitual();
        mAudioSource.PlayOneShot(mAudioClip);
    }

    private void ExtinctionSpel()
    {
        var alpha = mMaterial.color.a;
        alpha -= decAlphaValue * Time.deltaTime;
        mMaterial.color = new Color(
            mMaterial.color.r,
            mMaterial.color.g,
            mMaterial.color.b,
            alpha);

        if (alpha < 0)
        {
            mRenderer.enabled = false;
        }
    }
}
