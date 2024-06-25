using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHideController : MonoBehaviour
{
    public bool isHide = false;
    private string hideTag = "Hide";
    [SerializeField] private HidePointController currentHidePoint;

    private void Update()
    {
        if (currentHidePoint != null)
        {
            if (currentHidePoint.GetDestroy())
            {
                currentHidePoint = null;
                isHide = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == hideTag)
        {
            isHide = true;
            var hideObject = other.gameObject.transform.parent.gameObject;
            var hp = hideObject.GetComponent<HidePointController>();
   
            if (currentHidePoint == hp)
            {
                return;
            }
            else
            {
                currentHidePoint = hp;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == hideTag)
        {
            isHide = false;
            currentHidePoint = null;
        }
    }

    public HidePointController GetHidePoint()
    {
        return currentHidePoint;
    }
}
