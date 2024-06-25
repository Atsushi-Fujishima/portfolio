using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeController : MonoBehaviour
{
    public bool isEnableGaze = true;
    public bool isPenetration = false;
    public float rayDistance = 1.0f;
    public string targetLayerName = "Gaze";
    public TakeGazeController[] takeGazeControllers;
    private int layerMask = -1;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        layerMask = 1 << LayerMask.NameToLayer(targetLayerName);
    }

    private void Update()
    {
        if (isEnableGaze == false) return;

        var rayStartPosition = cam.transform.position;
        var rayDirection = cam.transform.forward.normalized;

        RaycastHit hit;

        if (isPenetration)
        {
            if (Physics.Raycast(rayStartPosition, rayDirection, out hit, rayDistance, layerMask))
            {
                foreach (var controller in takeGazeControllers)
                {
                    if (controller == null)
                    {
                        continue;
                    }

                    if (hit.collider.gameObject == controller.gameObject)
                    {
                        controller.TakeGaze();
                        break;
                    }
                }
            }
        }
        else
        {
            if (Physics.Raycast(rayStartPosition, rayDirection, out hit, rayDistance))
            {
                foreach (var controller in takeGazeControllers)
                {
                    if (controller == null)
                    {
                        continue;
                    }

                    if (hit.collider.gameObject == controller.gameObject)
                    {
                        controller.TakeGaze();
                        break;
                    }
                }
            }
        }

        

        Debug.DrawRay(rayStartPosition, rayDirection * rayDistance, Color.yellow);
    }
}
