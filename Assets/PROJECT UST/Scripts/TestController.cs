using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UST
{
    public class TestController : MonoBehaviour
    {
        public static TestController Instance { get; private set; } = null;

        public GameObject deadCanvas;
        public HurtHUD_UI hurtUI;

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

        public int currentHP;
        public int maxHP;
        public bool isDead;

        [Header("Camera")]
        public Camera playerCamera;
        [SerializeField] public float lookSpeed = 1f;
        public float lookXLimit = 45f;

        Vector3 moveDirection = Vector3.zero; //(Vector3 (0,0,0) 
        float rotationX = 0;

        public bool canMove = true;

        [SerializeField] CharacterController controller;
        public CrosshairUI crosshairUI;


        [Header("Health Point Recovery System")]
        public float recoveryDelayTime = 5f;
        public float recoveryDeltaTime = 0f;

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
            if (isGrounded && velocity.y < 0)
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
            if (canMove)
            {
                rotationX += Input.GetAxis("Mouse Y") * -1 * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }

            if (recoveryDeltaTime > 0f)
            {
                recoveryDeltaTime -= Time.deltaTime;
                if (recoveryDeltaTime <= 0)
                {
                    // 여기에 들어왔다는 뜻은 => 5초가 지나서 체력을 회복한다.
                    currentHP = maxHP;
                    hurtUI.SetAlpha(0);
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("R is pressed");
                SceneManager.LoadScene("GameStageTest");
                Time.timeScale = 1f;
            }

        }

        void Jump()
        {
            if ((Input.GetButtonDown("Jump")) && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity * jumpMultiplier);
                jumpMultiplier = 1f;
            }
        }

        public void TakeDamage(int damage)
        {
            damage = 1;
            currentHP -= damage;

            recoveryDeltaTime = recoveryDelayTime;

            float currentPersentage = (float)currentHP / maxHP;
            hurtUI.SetAlpha(1 - currentPersentage);

            if (currentHP <= 0)
            {
                // To do : Death
                isDead = true;
                deadCanvas.SetActive(true); 

                Time.timeScale = 0.0001f;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

             
            }
        }

    }
}
