using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SisterAppearanceController : MonoBehaviour
{
    public GameObject sister;

    private void Update()
    {
        var key = Keyboard.current;
        if (key.spaceKey.wasPressedThisFrame)
        {
            AppearanceSister();
            MyStaticMethod.DisplayColorLog("b", this.name, "appearanceSister", "");
        }
    }

    public void AppearanceSister()
    {
        if (sister.activeSelf)
            return;

        sister.SetActive(true);
    }
}
