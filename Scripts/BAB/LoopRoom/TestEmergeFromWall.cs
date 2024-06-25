using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEmergeFromWall : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject target;
    public float emergeDelay = 2.0f;
    public Transform setPlayerPoint;

    public void Emerge()
    {
        StartCoroutine(BlinkEyeSystem.instance.BlinkEye(SetPlayer, 0, true));
        StartCoroutine(ActiveCult());
    }

    private void SetPlayer()
    {
        playerTransform.position = setPlayerPoint.position;
        playerTransform.rotation = setPlayerPoint.rotation;
    }

    private IEnumerator ActiveCult()
    {
        yield return new WaitForSeconds(emergeDelay);

        target.SetActive(true);

        yield return new WaitForSeconds(4);

        target.SetActive(false);

        yield break;
    }
}
