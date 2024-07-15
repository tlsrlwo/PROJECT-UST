using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class TestController : MonoBehaviour
    {
        public static TestController Instance { get; private set; } = null;

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

        [Header("Camera")]
        public Camera playerCamera;
        [SerializeField]  public float lookSpeed = 1f;
        public float lookXLimit = 45f;

        Vector3 moveDirection = Vector3.zero; //(Vector3 (0,0,0) 
        float rotationX = 0;

        public bool canMove = true;

        [SerializeField] CharacterController controller;

        private void Awake()
        {
            Instance = this;
            controller = GetComponent<CharacterController>();

        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; 
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
            

            //rotation
            controller.Move(moveDirection * Time.deltaTime);
            if(canMove)
            {
                rotationX += Input.GetAxis("Mouse Y") * -1 * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }

     

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
