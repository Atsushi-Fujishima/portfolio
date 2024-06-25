using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultManager : MonoBehaviour
{
    [SerializeField] GameObject[] loops;
    [SerializeField] bool[] loopActivates;

    private void Start()
    {
        
    }

    private void Update()
    {
        for (int i = 0; i < loops.Length; i++)
        {
            if (IsSetCompleted(i))
            {
                SetCultCompleted(i);

            }
        }
    }

    private void SetCult(int loopIndex)
    {

    }

    private void SetCultCompleted(int loopIndex)
    {
        loopActivates[loopIndex] = true;
    }

    private bool IsSetCompleted(int loopIndex)
    {
        if (loopActivates[loopIndex]) 
            return true;
        else 
            return false;
    }
}
