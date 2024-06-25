using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype5
{
    public class HaiHaiController_5 : MonoBehaviour
    {
        [Header("Setting")]
        [SerializeField] float executableTime = 0.2f;
        [Header("Player")]
        [SerializeField] GameObject player;
        [SerializeField] Transform leftHandTransform;
        [SerializeField] Transform rightHandTransform;
        [SerializeField] HandCollisionController_5[] handCollisionControllers;
        [SerializeField] HandHapticManager handHapticManager;
        [SerializeField] PlayerBarrierController playerBarrierController;
        [Header("Value")]
        [SerializeField] float movePower = 5.0f;
        [SerializeField] float backPower = 2.0f;
        [SerializeField] float maxSpeed = 5.0f;
        [SerializeField] float deceoerationValue = 0.5f;
        [Range(0f, 1.0f)]
        [SerializeField] float advanceJudgmentValue = 0.3f;
        [SerializeField] float backJudgmentValue = 0.12f;
        [Space]
        [SerializeField] int addBarrierCount = 1;
        [Header("Component")]
        [SerializeField] BodyCollidedEffectController bodyCollidedEffectController;
        
        Rigidbody playerBody;
        PlayerSoundController_5 playerSoundController;
        Vector3 cameraForward;
        Transform cameraTransform;
        bool isMove;
        private string currentHand = string.Empty;
        private string previousHand = string.Empty;
        private float previousHandTime = 0f;
        private string[] handTypeNames = {"left", "right"};

        private void Start()
        {
            playerBody = GetComponent<Rigidbody>();
            playerSoundController = GetComponent<PlayerSoundController_5>();
            cameraTransform = Camera.main.transform;
        }

        private void FixedUpdate()
        {
            // カメラの方向から、X-Z平面の単位ベクトルを取得
            cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        }

        private void UpperSpeedLimit()
        {
            if (playerBody.velocity.sqrMagnitude > maxSpeed)
            {
                float newVx = playerBody.velocity.x * deceoerationValue;
                float newVz = playerBody.velocity.z * deceoerationValue;
                Vector3 newVelocity = new Vector3(newVx, playerBody.velocity.y, newVz);
                playerBody.velocity = newVelocity;
            }
        }

        private void HaiHaiMove(string dir)
        {
            float vectorPower;
            if (dir == "forward")
            {
                vectorPower = 1;
            }
            else
            {
                vectorPower = -backPower;
            }

            playerBody.velocity = Vector3.zero;
            Vector3 _force = cameraForward * movePower * vectorPower;
            playerBody.AddForce(_force, ForceMode.VelocityChange);
            if (bodyCollidedEffectController != null) 
                bodyCollidedEffectController.BodyCollidedEffectControl(vectorPower);
            UpperSpeedLimit();
        }

        public void AddHand(byte handType)
        {
            var currentHandTime = Time.time;
            var diffTime = currentHandTime - previousHandTime;

            if (handType == 0)
            {
                // left
                currentHand = handTypeNames[0];

                if (previousHand == handTypeNames[1])
                {
                    if (diffTime > executableTime)
                    {
                        //移動
                        HandPositionDetermination(handType);
                        if (playerBarrierController != null)
                            playerBarrierController.AddKillCount(addBarrierCount);

                        previousHandTime = currentHandTime; // update previous Time
                    }
                }

                previousHand = currentHand; // update previous hand
            }
            else
            {
                // right
                currentHand = handTypeNames[1];

                if (previousHand == handTypeNames[0])
                {
                    if (diffTime > executableTime)
                    {
                        //移動
                        HandPositionDetermination(handType);
                        if (playerBarrierController != null)
                            playerBarrierController.AddKillCount(addBarrierCount);

                        previousHandTime = currentHandTime; // update previous Time
                    }
                }

                previousHand = currentHand; // update previous hand
            }
        }

        private void HandPositionDetermination(byte handCode)
        {
            if (playerSoundController != null)
            {
                playerSoundController.PlayOneSoundStep();
            }

            if (handCode == 0) //左手を検知
            {
                Vector3 rightHandDirection = rightHandTransform.TransformDirection(Vector3.forward);
                Vector3 leftToOther = rightHandTransform.position - leftHandTransform.position;

                if (Vector3.Dot(rightHandDirection, leftToOther) < advanceJudgmentValue)
                {
                    //前進
                    HaiHaiMove("forward");
                }
                else if (Vector3.Dot(rightHandDirection, leftToOther) > backJudgmentValue)
                {
                    //後進
                    HaiHaiMove("back");
                }

                handHapticManager.LeftHandHaptic(0.2f, 0.2f);
            }
            else //右手を検知
            {
                Vector3 leftHandDirection = leftHandTransform.TransformDirection(Vector3.forward);
                Vector3 rightToOther = leftHandTransform.position - rightHandTransform.position;

                if (Vector3.Dot(leftHandDirection, rightToOther) < advanceJudgmentValue)
                {
                    //前進
                    HaiHaiMove("forward");
                }
                else if (Vector3.Dot(leftHandDirection, rightToOther)  > backJudgmentValue)
                {
                    //後進
                    //後進
                    HaiHaiMove("back");
                }

                handHapticManager.RightHandHaptic(0.2f, 0.2f);
            }
        }

        public bool GetIsMove()
        {
            if (playerBody.IsSleeping())
            {
                isMove = false;
            }
            else
            {
                isMove = true;
            }

            return isMove;
        }
    }
}