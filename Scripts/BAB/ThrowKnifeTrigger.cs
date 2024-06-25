using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowKnifeTrigger : MonoBehaviour
{
    private void OnDestroy()
    {
        KnifeThrowManager.instance.UpdateTrigger();
    }
}
