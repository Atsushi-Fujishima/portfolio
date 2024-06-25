using UnityEngine;
using UnityEngine.InputSystem;

public class HandRelocation : MonoBehaviour
{
    [SerializeField] InputActionReference inputKey;
    [Header("Relocation Point")]
    [SerializeField] Transform[] points;
    [Header("Hands")]
    [SerializeField] Transform[] hands;

    private void Update()
    {
        if (inputKey.action.WasPressedThisFrame())
        {
            MyStaticMethod.DisplayColorLog("c", this.name, "Hand Relocation", "");
            Relocation();
        }
    }

    private void Relocation()
    {
        hands[0].position = points[0].position;
        hands[1].position = points[1].position;
    }
}
