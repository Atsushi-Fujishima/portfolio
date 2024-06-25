using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CultMoverLoopRoom : MonoBehaviour
{
    [SerializeField] TalkTextController2 cultTalk;
    [SerializeField] Animator cultAnimator;
    [SerializeField] Transform moveDirationPoint;
    [SerializeField] CultLookPlayer lookPlayer;
    public float moveSpeed = 1.0f;
    public float rotationSpeed = 50.0f;
    private float turnAmount = 0f;
    [SerializeField] float turnAngle = 45f;
    [Header("Last Loop Room")]
    public bool isLastLoop = true;
    private bool isOneced = false;
    private bool isParmitMove = false;
    private bool isTurn = false;

    private void Update()
    {
        if (isLastLoop)
        {
            if (cultTalk.IsFinishTalk())
            {
                if (isOneced == false)
                {
                    isOneced = true;
                    lookPlayer.isLook = false;
                    cultAnimator.SetBool("Walk", true);
                    isParmitMove = true;
                    isTurn = true;
                }
            }

            if (isParmitMove)
            {
                MoveOut();
            }

            if (isTurn)
            {
                Turn();
            }
        }
    }

    private void MoveOut()
    {
        var dir = new Vector3(moveDirationPoint.position.x, transform.position.y, moveDirationPoint.position.z);
        var diff = dir - transform.position;
        diff = diff.normalized;

        transform.position += moveSpeed * Time.deltaTime * diff;
    }

    private void Turn()
    {
        var rotationAmount = rotationSpeed * Time.deltaTime;
        turnAmount += rotationAmount;

        if (turnAmount > turnAngle)
        {
            isTurn = false;
            moveSpeed = 1f;
        }
        else
        {
            var currentRotation = transform.rotation.eulerAngles;
            currentRotation += Vector3.down * rotationAmount;
            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }
}
