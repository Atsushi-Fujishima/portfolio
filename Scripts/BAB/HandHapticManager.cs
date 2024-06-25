using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandHapticManager : MonoBehaviour
{
    [Header("Action Based Controller")]
    [SerializeField] ActionBasedController l_Controller;
    [SerializeField] ActionBasedController r_Controller;


    private ActionBasedController useLController = null;
    private ActionBasedController useRController = null;
    private string typeLeft = "left";
    private string typeRight = "right";

    public void Start()
    {
        useLController = l_Controller;
        useRController = r_Controller;
    }

    public void LeftHandHaptic(float _amplitude, float _duration)
    {
        useLController = l_Controller;
        useLController.SendHapticImpulse(_amplitude, _duration);
        useLController = null;
    }

    public void RightHandHaptic(float _amplitude, float _duration)
    {
        useRController = r_Controller;
        useRController.SendHapticImpulse(_amplitude, _duration);
        useRController = null;
    }

    public void BothHandHaptic(float _amplitude, float _duration)
    {
        useLController = l_Controller;
        useRController = r_Controller;
        useLController.SendHapticImpulse(_amplitude, _duration);
        useRController.SendHapticImpulse(_amplitude, _duration);
        useLController = null;
        useRController = null;
    }

    private void HapticInitialization(string type)
    {
        if (type == typeLeft)
        {
            l_Controller.SendHapticImpulse(0, 0);
        }
        else if (type == typeRight)
        {
            r_Controller.SendHapticImpulse(0, 0);
        }
        else
        {
            l_Controller.SendHapticImpulse(0, 0);
            r_Controller.SendHapticImpulse(0, 0);
        }
    }

    
}
