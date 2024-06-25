using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrowManager : MonoBehaviour
{
    static public KnifeThrowManager instance;
    [SerializeField] GameObject knifeOrigin;
    [SerializeField] Animator dollAnimator;
    [SerializeField] Transform parent;
    public int triggerNum = 3;
    private int currentNum = 0;
    private bool isThorwEnable = false;
    private float throwCoolTime = 3f;
    private float elapsedTime = 0f;
    private GameObjectThrowController throwController = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (isThorwEnable)
        {
            if (dollAnimator.GetBool("HandUp") == false)
            {
                ThrowControl();
            }
        }
    }

    public void CallThorw()
    {
        if (throwController == null) return;
        throwController.Throw();
        throwController = null;
        dollAnimator.SetBool("HandUp", false);
    }

    private void SetKnife()
    {
        GameObject knife = Instantiate(knifeOrigin, knifeOrigin.transform.position, knifeOrigin.transform.rotation, parent);
        knife.SetActive(true);
        throwController = knife.GetComponent<GameObjectThrowController>();
    }

    private void ThrowControl()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > throwCoolTime)
        {
            elapsedTime = 0f;
            dollAnimator.SetBool("HandUp", true);
            SetKnife();
        }
    }

    public void UpdateTrigger()
    {
        if (isThorwEnable) return;

        currentNum++;
        if (currentNum >= triggerNum)
        {
            isThorwEnable = true;
        }
    }
}
