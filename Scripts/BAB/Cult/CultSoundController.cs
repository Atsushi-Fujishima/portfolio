using UnityEngine;

public class CultSoundController : MonoBehaviour
{
    [SerializeField] SoundEffectNaturalExpression soundEffectExpression;

    public void PlayCultSound()
    {
        soundEffectExpression.PlaySound();
    }
}
