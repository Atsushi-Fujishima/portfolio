using System.Collections;
using UnityEngine;

public class MagicRoomEventController : MonoBehaviour
{
    [Header("Event Control")]
    [SerializeField] TalkEventController talkEventController;
    [Header("Player")]
    [SerializeField] Transform playerTransform;
    [Header("Sister")]
    [SerializeField] GameObject sister;
    [SerializeField] Transform sisterHead;
    [SerializeField] SkinnedMeshRenderer[] sisterRenderers;
    [SerializeField] Vector3 front = Vector3.right;
    [SerializeField] float disappearSpeed = 1.0f;
    private Material[] mat_sisters = new Material[2];
    private string transparentShaderName = "_Tweak_transparency";

    private void Start()
    {
        mat_sisters[0] = sisterRenderers[0].material;
        mat_sisters[1] = sisterRenderers[1].material;
    }

    private void Update()
    {
        if (sisterHead.gameObject.activeSelf)
            LookPlayer();
    }

    private void LookPlayer()
    {
        Vector3 direction = playerTransform.position - sisterHead.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        Quaternion offsetRotaiton = Quaternion.FromToRotation(front, Vector3.forward);
        sisterHead.rotation = lookRotation * offsetRotaiton;
    }

    public void CallSisterDisappear()
    {
        StartCoroutine(SisterDisappear());
    }

    private IEnumerator SisterDisappear()
    {
        while (mat_sisters[0].GetFloat(transparentShaderName) > -0.9f)
        {
            foreach (var mat_sister in mat_sisters)
            {
                float tValue = mat_sister.GetFloat(transparentShaderName);
                tValue -= disappearSpeed * Time.deltaTime;
                mat_sister.SetFloat(transparentShaderName, tValue);
            }
   
            yield return null;
        }

        foreach (var mat_sister in mat_sisters)
        {
            mat_sister.SetFloat(transparentShaderName, -1.0f);
        }

        sisterRenderers[1].enabled = false;
       
        yield break;
    }

    public void InteractionBookSisterAppears()
    {
        StartCoroutine(SisterAppears());    
    }

    private IEnumerator SisterAppears()
    {
        talkEventController.CallStartEvent();
        yield return new WaitForSeconds(2);
        sister.SetActive(true);
        yield break;
    }
}
