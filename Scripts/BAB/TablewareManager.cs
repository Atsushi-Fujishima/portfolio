using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TablewareManager : MonoBehaviour
{
    public enum TablewareType
    {
        Eating,
        Other
    }

    public TablewareType type = TablewareType.Eating;
    public Transform foodPoint = null;
    public Transform tipPoint = null;
    [Header("Drop")]
    [SerializeField] private float dropTriggerTime = 1.0f;
    private float elapsedTriggerTime = 0f;
    public bool isGrab = false;
    private BabFoodController getFoodController = null;
    private readonly int changedLayer = 18;
    private readonly int defaultLayer = 0;

    private void Update()
    {
        if (getFoodController != null) DropControl();
    }

    public void EnterGrab()
    {
        isGrab = true;
        gameObject.layer = changedLayer;
    }

    public void ExitGrab()
    {
        isGrab = false;
        gameObject.layer = defaultLayer;
        FoodDrop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (type != TablewareType.Eating) return;
        if (isGrab == false) return;
        if (getFoodController != null) return;

        if (collision.gameObject.tag == "Food")
        {
            GameObject food = collision.gameObject;
            getFoodController = food.GetComponent<BabFoodController>();
            getFoodController.Get(foodPoint);
        }
    }

    private void DropControl()
    {
        if (tipPoint.transform.forward.y < 0)
        {
            elapsedTriggerTime += Time.deltaTime;
            if (elapsedTriggerTime > dropTriggerTime)
            {
                FoodDrop();
                elapsedTriggerTime = 0f;
            }
        }
        else
        {
            elapsedTriggerTime = 0f;
        }
    }

    private void FoodDrop()
    {
        if (getFoodController == null) return;

        getFoodController.Drop();
        StartCoroutine(PermitGetUpdate());
    }

    private IEnumerator PermitGetUpdate()
    {
        yield return new WaitForSeconds(0.5f);
        getFoodController = null;
        yield break;
    }
}
