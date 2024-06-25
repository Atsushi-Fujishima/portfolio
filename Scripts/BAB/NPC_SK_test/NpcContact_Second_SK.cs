using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcContact_Second_SK : MonoBehaviour
{
    private bool isItemInteract = false;
    private bool isPermitTalk = false;
    [Header("EventItems")]
    [SerializeField] InteractionItemController[] itemControllers;
    public int interactLimit = 4;
    private int interactCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isItemInteract)
        {
            if (interactCount > interactLimit)
            {
                isItemInteract = false;
                isPermitTalk = true;
            }
        }
    }

    public void EnableItems()
    {
        isItemInteract = true;
        foreach (var item in itemControllers)
        {
            item.EnableItem();
        }
    }

    public void AddInteractCount()
    {
        interactCount++;
    }

    public void OnSecondContact()
    {
        if (isPermitTalk == false) return;
        StartCoroutine(SecondContact());
    }

    private IEnumerator SecondContact()
    {
        isPermitTalk = false;
        yield break;
    }
}
