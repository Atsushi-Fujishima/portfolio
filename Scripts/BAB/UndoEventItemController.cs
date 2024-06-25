using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UndoEventItemController : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] Transform targetTransform;
    [SerializeField] Transform targetScaleTransform;
    [Header("Undo Set Value")]
    [SerializeField] Vector3 undoPosition = Vector3.zero;
    [SerializeField] Vector3 undoRotation = Vector3.zero;
    [SerializeField] Vector3 undoScale = Vector3.one;
    [Header("Is Statues Control")]
    [SerializeField] ObjectStatuesController statuesController;
    [Header("Related Game objects")]
    [SerializeField] Transform[] relatedTransforms;
    [SerializeField] Vector3[] relatedUndoLocalPositions;

    public void UndoEventItem()
    {
        DisableStatuesController();
        UndoRelatedGameObject();

        targetTransform.localPosition = undoPosition;
        targetTransform.localRotation = Quaternion.Euler(undoRotation);
        
        if (targetScaleTransform != null)
        {
            targetScaleTransform.localScale = undoScale;
        }
        else
        {
            targetTransform.localScale = undoScale;
        }
    }

    private void DisableStatuesController()
    {
        if (statuesController != null)
        {
            statuesController.DisableStatuesControl();
        }
    }

    private void UndoRelatedGameObject()
    {
        if (relatedTransforms.Length < 1) return;

        for (int i = 0; i < relatedTransforms.Length; i++)
        {
            relatedTransforms[i].localPosition = relatedUndoLocalPositions[i];
        }
    }
}
