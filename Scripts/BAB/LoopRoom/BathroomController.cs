using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomController : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] Animator doorAnim;
    [Header("StateNames")]
    [SerializeField] private string[] stateNames = { "Close", "HalfOpen", "Open", "Knock" };
    [Header("SubLight")]
    [SerializeField] Light subLight;

    private void Start()
    {
        CloseDoor();
    }

    public void CloseDoor()
    {
        doorAnim.Play(stateNames[0]);
        subLight.enabled = false;
    }

    public void HalfOepnDoor()
    {
        doorAnim.Play(stateNames[1]);
        subLight.enabled = false;
    }

    public void OpenDoor()
    {
        doorAnim.Play(stateNames[2]);
        subLight.enabled = true;
    }

    public void KnockDoor()
    {
        doorAnim.Play(stateNames[3]);
        subLight.enabled = false;
    }
}
