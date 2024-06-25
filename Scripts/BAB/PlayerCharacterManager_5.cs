using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Prototype5
{
    public class PlayerCharacterManager_5 : MonoBehaviour
    {
        [SerializeField] PlayerInputActionManager inputManager;
        [SerializeField] InputActionReference actionLeftTrigger;
        [SerializeField] InputActionReference actionRightTrigger;
        [SerializeField] Transform characterTransform;
        [SerializeField] Transform cameraTransform;
        [SerializeField] Transform leftHandTransform;
        [SerializeField] Transform rightHandTransform;

        [Space]
        [SerializeField] float distanceFromCenter = 0.3f;

        [Space]
        bool isDetection = false;
        [SerializeField, Range(0.1f, 1.0f)] float delayDetectionTime = 0.1f;
        float delayDetectionCount = 0f;

        PostureController_5 postureController;
        HaiHaiRotate_5 haiHaiRotate;
        HaiHaiController_5 haiController;
        HandCollidedManager handCollidedManager;
        StaminaManager staminaManager;
        PlayerRotationManager playerRotationManager;

        [Header("Coercion")]
        public bool isNotRotate = false;
        public bool isNotMove = false;

        public enum RotateType
        {
            Internal,
            Extarnal
        }
        [Header("RotateType")]
        [SerializeField] public RotateType rotateType = RotateType.Internal;

        bool isStand;
        bool isHiHiRotate;
        bool isMove;
        bool isGround;

        private void Start()
        {
            postureController = GetComponent<PostureController_5>();
            haiHaiRotate = GetComponent<HaiHaiRotate_5>();
            haiController = GetComponent<HaiHaiController_5>();
            handCollidedManager = GetComponent<HandCollidedManager>();
            //staminaManager = GetComponent<StaminaManager>();
            playerRotationManager = GetComponent<PlayerRotationManager>();
        }

        private void Update()
        {
            
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                isGround = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                isGround = false;
            }
        }

        public void BehaviourController(byte _handCode)
        {
            if (actionLeftTrigger.action.IsPressed() || actionRightTrigger.action.IsPressed())
            {
                return;
            }

            if (rotateType == RotateType.Internal)
            {
                //接地位置の検知 left
                Vector3 leftHandDirection = characterTransform.TransformDirection(Vector3.right);
                Vector3 leftToOther = characterTransform.position - leftHandTransform.position;
                float leftHandDot = Vector3.Dot(leftHandDirection, leftToOther);
                float leftHandDirectionAbs = Mathf.Abs(leftHandDot);

                //接地位置の検知 right
                Vector3 rightHandDirection = characterTransform.TransformDirection(Vector3.right);
                Vector3 rightToOther = characterTransform.position - rightHandTransform.position;
                float rightHandDot = Vector3.Dot(rightHandDirection, rightToOther);
                float rightHandDirectionAbs = Mathf.Abs(rightHandDot);

                if (leftHandDirectionAbs > distanceFromCenter || rightHandDirectionAbs > distanceFromCenter)
                {
                    RotationBehaviourControl(leftHandDot, rightHandDot, _handCode);
                }
                else
                {
                    
                }

                if (isNotMove) return;
                //移動
                haiController.AddHand(_handCode);
                //staminaManager.DecreaseStamina();
            }
            else
            {
                if (isNotMove) return;
                //移動
                haiController.AddHand(_handCode);
                //staminaManager.DecreaseStamina();
            }
        }

        private void RotationBehaviourControl(float _LHandDot, float _RHandDot, byte _HandType)
        {
            if (handCollidedManager.PermitRotate() && isNotRotate == false)
            {
                playerRotationManager.SetHandData(_LHandDot, _RHandDot, _HandType);
                playerRotationManager.PlayerCharacterRotation();
                handCollidedManager.StartPseudoRotate();
            }
        }

        private void DelayDetection()
        {
            if (isDetection)
            {
                delayDetectionCount += Time.deltaTime;
                if (delayDetectionCount > delayDetectionTime)
                {
                    isDetection = false;
                    delayDetectionCount = 0f;
                }
            }
        }

        public bool GetIsStand()
        {
            return isStand;
        }

        public bool GetIsHiHiRotate()
        {
            return isHiHiRotate;
        }

        public bool GetIsMove()
        {
            return isMove;
        }

        public bool GetIsGround()
        {
            return isGround;
        }
    }
}