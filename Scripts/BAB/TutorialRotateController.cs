using UnityEngine;

public class TutorialRotateController : MonoBehaviour
{
    [SerializeField] TutorialController controller;
    [SerializeField] PlayerRotationStick playerRotationStick;
    [SerializeField] HandHapticManager handHapticManager;
    private bool isCompletion = false;
    private bool[] compRotations = new bool[2];

    private void Update()
    {
        if (isCompletion == false)
        {
            Reception();
        }
    }

    private void Reception()
    {
        if (compRotations[0] && compRotations[1])
        {
            isCompletion = true;
            controller.CompletionTutorial();
            return;
        }

        if (playerRotationStick.GetRotate())
        {
            if (playerRotationStick.GetReadRotationDirection() == "Left")
            {
                compRotations[0] = true;
                handHapticManager.LeftHandHaptic(1.0f, 0.5f);
            }
            else if (playerRotationStick.GetReadRotationDirection() == "Right")
            {
                compRotations[1] = true;
                handHapticManager.RightHandHaptic(1.0f, 0.5f);
            }
            else
            {
                return;
            }
        }
    }
}
