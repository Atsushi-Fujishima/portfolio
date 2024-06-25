using System.Collections;
using UnityEngine;

public class PlayerInteractedManager : MonoBehaviour
{
    [Header("Hands")]
    [SerializeField] GameObject[] hands;
    [SerializeField] int handLayerIndex;
    [Header("Gaze")]
    [SerializeField] GazeController gazeController;

    private bool isInteracted = false;
    private float delay = 3f;

    public bool IsInteracted()
    {
        return isInteracted;
    }

    public void Interacted()
    {
        StartCoroutine(InteractedControl());
    }

    private IEnumerator InteractedControl()
    {
        isInteracted = true;
        yield return new WaitForSeconds(delay);
        isInteracted = false;
        yield break;
    }

    public void InteractionInitialize()
    {
        isInteracted = false;
    }

    public void DisableInteraction()
    {
        foreach (var hand in hands)
        {
            hand.layer = 21;
        }

        gazeController.isEnableGaze = false;
    }

    public void EnableInteraction()
    {
        foreach (var hand in hands)
        {
            hand.layer = handLayerIndex; // default
        }

        gazeController.isEnableGaze = true;
    }
}
