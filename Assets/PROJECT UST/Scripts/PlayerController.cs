using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 3.0f;
        public float sprintSpeed = 5.0f;
        public float speedChangeRate = 10.0f;

        public float cameraHorizontalSpeed = 2.0f;
        public float cameraVerticalSpeed = 2.0f;

        [Range(0.0f, 0.3f)] public float rotationSmoothTime = 0.12f;

        public float topClamp = 70.0f;
        public float bottomClamp = -30.0f;
        public GameObject cinemachineCameraTarget;
        public float cameraAngleOverride = 0.0f;

        private Animator animator;
        private Camera mainCamera;
        private CharacterController controller;

        private bool isSprint = false;
        private Vector2 move;
        private float speed;
        private float animationBlend; //
        private float targetRotation = 0.0f;
        private float rotationVelocity;
        private float verticalVelocity;

        private Vector2 look;
        private const float _threshold = 0.01f;
        private float cinemachineTargetYaw;
        private float cinemachineTargetPitch;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            controller = GetComponent<CharacterController>();
            mainCamera = Camera.main;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            move = new Vector2(horizontal, vertical);

            float hMouse = Input.GetAxis("Mouse X");
            float vMouse = Input.GetAxis("Mouse Y") * -1; //-1을 곱하는 이유는 상하반전을 일으킴(마우스 위아래 움직임)
            look = new Vector2(hMouse, vMouse);

            isSprint = Input.GetKey(KeyCode.LeftShift);

            Move();

            animator.SetFloat("Speed", animationBlend);

        }
        private void LateUpdate()
        {
            CameraRotation();
        }

        private void CameraRotation()
        {
            if (look.sqrMagnitude >= _threshold)
            {
                float deltaTimeMultiplier = 1.0f;

                cinemachineTargetYaw += look.x * deltaTimeMultiplier * cameraHorizontalSpeed;
                cinemachineTargetPitch += look.y * deltaTimeMultiplier * cameraVerticalSpeed;
            }

            cinemachineTargetYaw = ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);
            cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, bottomClamp, topClamp);

            cinemachineCameraTarget.transform.rotation = Quaternion.Euler(cinemachineTargetPitch + cameraAngleOverride,
                                                         cinemachineTargetYaw, 0.0f);
        }
     

        private void Move()
        {           
            float targetSpeed = isSprint ? sprintSpeed : moveSpeed;

            if (move == Vector2.zero) targetSpeed = 0.0f;

            float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;
            float speedOffset = 0.1f;
            float inputMagnitude = move.magnitude;

            if(currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * speedChangeRate);

                speed = Mathf.Round(speed * 1000f) / 1000f;
            }
            else
            {
                speed = targetSpeed;
            }

            //blend = 전환
            animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);
            if (animationBlend < 0.01f) animationBlend = 0f;

            //normalize input direction
            Vector3 inputDirection = new Vector3(move.x, 0.0f, move.y).normalized;

            if(move != Vector2.zero)
            {
                targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
                
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity,
                                 rotationSmoothTime);

                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

            controller.Move(targetDirection.normalized * (speed * Time.deltaTime) +
                            new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);

        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}
