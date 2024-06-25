using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyVisibleController : MonoBehaviour
{
    public GameObject target;
    [SerializeField] ObjectVisible objectVisible = null;

    private void Update()
    {
        if (objectVisible.GetIsShow() == false)
        {
            target.SetActive(false);
        }
    }
}
