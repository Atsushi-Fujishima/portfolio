using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionItemController : MonoBehaviour
{
    public bool isAwake = false;
    [SerializeField] ParticleSystem pickup;

    private PlayerTriggerEventController playerTriggerEventController;

    private void Start()
    {
        if (GetComponent<PlayerTriggerEventController>() != null)
        {
            playerTriggerEventController = GetComponent<PlayerTriggerEventController>();
        }

        if (isAwake)
        {
            EnableItem();
        }
    }

    public void EnableItem()
    {
        pickup.Play();
    }

    public void DisableItem()
    {
        pickup.Stop();

        if (playerTriggerEventController != null && playerTriggerEventController.isOneced == false)
        {
            EnableItem();
        }
    }
}
