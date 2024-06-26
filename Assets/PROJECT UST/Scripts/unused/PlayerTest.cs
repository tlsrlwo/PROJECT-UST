using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class PlayerTest : MonoBehaviour
    {
        [Header("Player Move")]
        public float moveSpeed = 3;
        [HideInInspector] public Vector3 dir;
        float hzInput, vInput;
        CharacterController controller;

        [Header("Gravity")]
        [SerializeField] float groundYOffset;
        [SerializeField] LayerMask groundMask;
        Vector3 spherePos;

        [SerializeField] float gravity = -9.81f;
        Vector3 velocity;

        [Header("Animation")]
        private Animator animator;


        private bool isSprint = false;
        private Vector2 move;
        private float speed;
        private float animationBlend;

        private bool isEnableMovement = true;
        private bool isStrafe;       



        private void Start()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();

        }
        private void Update()
        {
            PlayerMove();
            Gravity();

            isSprint = Input.GetKey(KeyCode.LeftShift);
            if(isSprint)
            {
                animator.SetFloat("MotionSpeed", 1.25f);
            }
            else
            {
                animator.SetFloat("MotionSpeed", 1f);
            }

            isStrafe = Input.GetKey(KeyCode.Mouse1);
            if (isStrafe)
            {
                Vector3 cameraForward = Camera.main.transform.forward.normalized;
                cameraForward.y = 0;
                transform.forward = cameraForward;
                
            }



            animator.SetFloat("Speed", animationBlend);
            animator.SetFloat("Horizontal", move.x);
            animator.SetFloat("Vertical", move.y);
            animator.SetFloat("Strafe", isStrafe ? 1 : 0);
        }


        void PlayerMove()
        {
            hzInput = Input.GetAxis("Horizontal");
            vInput = Input.GetAxis("Vertical");

            dir = transform.forward * vInput + transform.right * hzInput;

            controller.Move(dir * moveSpeed * Time.deltaTime);
        }

        bool IsGrounded()
        {
            spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
            if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
            return false;

        }

        void Gravity()
        {
            if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
            else if (velocity.y < 0) velocity.y = -2;

            controller.Move(velocity * Time.deltaTime);
        }

       

    }
}
