using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HorrorEffectController : MonoBehaviour
{
    public UnityEvent OnCallHorrorEffect;

    public void CallEffect()
    {
        OnCallHorrorEffect?.Invoke();
    }
}
