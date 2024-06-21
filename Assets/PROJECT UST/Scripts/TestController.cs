using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class TestController : MonoBehaviour
    {
        [Header("Player Move")]
        public float speed = 6f;
        public float jumpHeight = 3f;
        private float jumpMultiplier = 1f;
        public float gravity = -9.81f;
        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;
        Vector3 velocity;
        bool isGrounded;
                

        [SerializeField] CharacterController controller;

        private void Awake()
        {
            
        }


        private void Update()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if(isGrounded && velocity.y < 0)
            {
                speed = 6f;
                velocity.y = -2f; 
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            Jump();

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);            

        }
        void Jump()
        {
            if((Input.GetButtonDown("Jump")) && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity * jumpMultiplier);
                jumpMultiplier = 1f;

            }

        }



    }
}
