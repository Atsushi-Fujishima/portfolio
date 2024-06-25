using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSisterChaseEvent : MonoBehaviour
{
    public AudioSource bgmAudio;
    public AudioSource eventAudio;
    public AudioClip _clip;
    [SerializeField] SisterMoveController _moveController;
    private bool isOneced = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isOneced == false)
            {
                isOneced = true;
                StartCoroutine(EventStart());
            }
        }
    }

    private IEnumerator EventStart()
    {
        yield return new WaitForSeconds(1.0f);
        StartChase();
        yield break;
    }

    private void StartChase()
    {
        eventAudio.PlayOneShot(_clip);
        bgmAudio.Play();
        _moveController.SetDestinationPlayer();
    }
}
