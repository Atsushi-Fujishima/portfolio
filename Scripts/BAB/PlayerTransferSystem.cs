using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTransferSystem : MonoBehaviour
{
    public int sceneIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Transfer();
        }
    }

    private void Transfer()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
