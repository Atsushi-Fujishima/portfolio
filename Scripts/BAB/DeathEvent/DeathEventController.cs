using UnityEngine;

public class DeathEventController : MonoBehaviour
{
    [SerializeField] PlayerLoopTeleport playerLoopTeleport;
    [SerializeField] PlayerDeathTeleport playerDeathTeleport;
    [SerializeField] PlayerInteractedManager playerInteractedManager;
    [Header("Noraml Sister")]
    [SerializeField] DeathEventNoramlType normalEvent;
    [Header("Special Sister")]
    [SerializeField] DeathEventSpecialType specialEvent;
    

    private string[] eventTypeCode = { "Noraml", "Special" };
    private SphereCollider setCurrentSisterCollider;
    private SisterVoiceController setSisterVoiceController;

    public void StartDeathEvent(string _eventTypeCode, SphereCollider setCollider, SisterVoiceController setVoice)
    {
        setCurrentSisterCollider = setCollider;
        setSisterVoiceController = setVoice;

        if (_eventTypeCode == eventTypeCode[0])
        {
            normalEvent.CallNormalSiterDeathEventStart();
        }
        else
        {
            specialEvent.CallSpecialSiterDeathEventStart();
            MyStaticMethod.DisplayColorLog("r", this.name, "StartDeathevent", _eventTypeCode.ToString());
        }
    }

    public void EndDeathEvent()
    {
        playerDeathTeleport.DeathInitialized();
        setCurrentSisterCollider.enabled = true;
        playerLoopTeleport.PlayerCharacterTeleport();
        setSisterVoiceController.PlaySoundSoli();
        playerInteractedManager.EnableInteraction(); // enable player interaction
    }

    public string NormalEventCode()
    {
        return eventTypeCode[0];
    }

    public string SpecialEventCode()
    {
        return eventTypeCode[1];
    }
}
