using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabuDisappear : MonoBehaviour
{
    public bool isDisappear = false;

    public void EnableDisappear()
    {
        isDisappear = true;
    }

    private void OnBecameInvisible()
    {
        if (isDisappear) gameObject.SetActive(false);
    }
}
