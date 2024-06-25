using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDevDollEyeControl : MonoBehaviour
{
    public Transform target;
    public Transform[] eyes;
    public Vector3 forward = Vector3.forward;

    private void Update()
    {
        if (target != null)
        {
            foreach (Transform t in eyes)
            {
                var dir = target.position - t.position;

                var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);

                var offsetRotation = Quaternion.FromToRotation(forward, Vector3.forward);

                t.rotation = lookAtRotation * offsetRotation;
            }
        }
    }
}
