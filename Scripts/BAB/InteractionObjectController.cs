using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObjectController : MonoBehaviour
{
    [Header("Setting")]
    public bool isAwake = false;
    [Header("Renderer")]
    [SerializeField] private MeshRenderer mr;
    [Header("Materials")]
    [SerializeField] private Material matNormal;
    [SerializeField] private Material matInteract;

    private PlayerTriggerEventController playerTriggerEventController;
    
    private void Start()
    {
        playerTriggerEventController = GetComponent<PlayerTriggerEventController>();

        if (isAwake) EnableInteract();
    }

    public void EnableInteract()
    {
        mr.material = matInteract;
    }

    public void ExecutionInteract()
    {
        mr.material = matNormal;

        if (playerTriggerEventController != null && playerTriggerEventController.isOneced == false)
        {
            EnableInteract();
        }
    }
}
