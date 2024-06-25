using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseRattleController : MonoBehaviour
{
    public bool isUseRattle = false;
    [SerializeField] GameObject rattle;

    private void Update()
    {
        if (isUseRattle) 
        { 
            if (rattle.activeSelf == false) rattle.SetActive(true);
        } 
    }

    public void ActiveRattle()
    {
        isUseRattle = true;
    }
}
