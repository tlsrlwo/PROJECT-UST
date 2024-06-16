using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Animations.Rigging 선언해도 됨 밑에 public Rig

namespace UST
{
    public class PlayerController : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(aimTarget.position, 0.1f);
        }


        public bool IsEnableMovement
        {
            set => isEnableMovement = value;
        }
        //get, set 이 붙는 애들은 프로퍼티(properties) => 외부에서 접근 가능하게 만듦

        //필드 (변수를 필드라고 부름)
        [Header("Character Setting")]
        public float moveSpeed = 3.0f;
        public float sprintSpeed = 5.0f;
        public float speedChangeRate = 10.0f;

        [Header("Camera Setting")]
        public float cameraHorizontalSpeed = 2.0f;
        public float cameraVerticalSpeed = 2.0f;

        [Range(0.0f, 0.3f)] public float rotationSmoothTime = 0.12f;

        [Header("Sliding Setting")]
        public AnimationCurve slidingCurve;

        [Header("Weapon Holder")]
        public GameObject weaponHolder;

        [Header("Weapon FOV")]
        public float defaultFOV;
        public float aimFOV;

        [Header("Camera Clamping")]
        public float topClamp = 70.0f;
        public float bottomClamp = -30.0f;
        public GameObject cinemachineCameraTarget;
        public float cameraAngleOverride = 0.0f;

        [Header("Animation Rigging")]
        public Transform aimTarget;
        public LayerMask aimingLayer;
        public UnityEngine.Animations.Rigging.Rig aimingRig; //맨 위에 참고 Using.Unity 쓰는데

        public float aimingIKBlendCurrent;
        public float aimingIKBlendTarget;

        private Animator animator;
        private Camera mainCamera;
        private CharacterController controller;
       /* private InteractionSensor interactionSensor;
        private Weapon currentWeapon;
        private AnimationEventListener animationEventListener;*/

        private bool isSprint = false;
        private Vector2 move;
        private float speed;
        private float animationBlend;
        private float targetRotation = 0.0f;
        private float rotationVelocity;
        private float verticalVelocity;

        private Vector2 look;
        private const float _threshold = 0.01f;
        private float cinemachineTargetYaw;
        private float cinemachineTargetPitch;

        private bool isEnableMovement = true;
        private bool isStrafe;
        private bool isReloading = false; //reload 중 인지 여부

        private void Awake()
        {
            //getcomponent = 자기자신 오브젝트 getcomponentinchildren => 자기자신의 한단계 아래 컴포넌트
            animator = GetComponentInChildren<Animator>();
            controller = GetComponent<CharacterController>();
            mainCamera = Camera.main;
           /* interactionSensor = GetComponentInChildren<InteractionSensor>();
            animationEventListener = GetComponentInChildren<AnimationEventListener>();
            animationEventListener.OnTakenAnimationEvent += OnTakenAnimationEvent;

            var weaponGameObject = TransformUtility.FindGameObjectWithTag(weaponHolder, "Weapon");
            currentWeapon = weaponGameObject.GetComponent<Weapon>();*/
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            Cursor.visible = false;
        }

      /*  private void OnEnable()
        {
            interactionSensor.OnDetected += OnDetectedInteraction;
            interactionSensor.OnLost += OnLostInteraction;
        }
        private void OnDetectedInteraction(IInteractable interactable)
        {
            InteractionUI.Instance.AddInteractionData(interactable);
        }


        private void OnLostInteraction(IInteractable interactable)
        {
            InteractionUI.Instance.RemoveInteractionData(interactable);
        }*/


        private void Update()
        {           

            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Trigger_Slide");
            }

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            move = new Vector2(horizontal, vertical);


            float hMouse = Input.GetAxis("Mouse X");
            float vMouse = Input.GetAxis("Mouse Y") * -1; //-1을 곱하는 이유는 상하반전을 일으킴(마우스 위아래 움직임)
            look = new Vector2(hMouse, vMouse);

            isSprint = Input.GetKey(KeyCode.LeftShift); //달리는 속도에 따라 달리는 애니메이션이 곱으로 빨라지는 모션
            if (isSprint)
            {
                animator.SetFloat("MotionSpeed", 1.25f);
            }
            else
            {
                animator.SetFloat("MotionSpeed", 1f);
            }
            isStrafe = Input.GetKey(KeyCode.Mouse1); //우클릭

            if (isStrafe) //캐릭터의 방향을 우클릭시(하는동안) 카메라의 방향으로 고정
            {
                Vector3 cameraForward = Camera.main.transform.forward.normalized;
                cameraForward.y = 0;
                transform.forward = cameraForward;
            }

     /*       if (Input.GetKey(KeyCode.Mouse0)) //Mouse Left
            {
                if (currentWeapon != null)
                {
                    currentWeapon.Shoot();
                }
                currentWeapon?.Shoot();
                // currentWeapon?.Shoot();  //위에꺼랑 똑같은데 이것도 가능, 옛날 유니티에서는 안됨

                var cameraForward = Camera.main.transform.forward.normalized;
                cameraForward.y = 0;
                transform.forward = cameraForward;
            }
*/
        /*    if (Input.GetKeyDown(KeyCode.Mouse1)) //mouse right duplicated with strafe
            {
                //zoom in
                CameraSystem.Instance.TargetFOV = aimFOV;
                aimingIKBlendTarget = isReloading ? 0f : 1f; //reloading 이면 ik를 올리지 말고 아니면 ik를 올림
            }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                //zoom out
                CameraSystem.Instance.TargetFOV = defaultFOV;
                aimingIKBlendTarget = 0f;
            }*/

            //장전reload
           /* if (Input.GetKeyDown(KeyCode.R))
            {
                animator.SetLayerWeight(1, 1f); //layer1번(BaseLayer가 0번)에 (weight)1로 설정
                animator.SetTrigger("Trigger_Reload");
                isReloading = true;
            }*/


          /*  aimingIKBlendCurrent = Mathf.Lerp(aimingIKBlendCurrent, aimingIKBlendTarget, Time.deltaTime * 10f);
            aimingRig.weight = aimingIKBlendCurrent;*/

            /*if(CameraSystem.Instance.IsTPSMode == false)
            {
                Vector3 fpsForward = mainCamera.transform.forward;
                fpsForward.y = 0;
                transform.forward = fpsForward;
            }*/ // 안되서 안씀

            Move();

            animator.SetFloat("Speed", animationBlend);
            animator.SetFloat("Horizontal", move.x);
            animator.SetFloat("Vertical", move.y);
            animator.SetFloat("Strafe", isStrafe ? 1 : 0);

            // 카메라에 ViewportPointToRay 를 이용하여 화면 중앙을 바라보는 Ray를 생성
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            // Raycast 를 이용하여 화면 중앙을 바라보는 Ray와 충돌된 대상의 정보를 저장
            //Vector3 aimingTargetPosition = Vector3.zero;
            Vector3 aimingTargetPosition = Camera.main.transform.position + Camera.main.transform.forward * 1000f;

            // Raycast 가 성공하면 aimingTargetPosition 을 Raycast가 성공한 지점으로 설정
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, aimingLayer))
            {
                if (hitInfo.transform.root != transform) //raycast 에 hit 처리된 것의 root 의 info 가 나면 
                {
                    aimingTargetPosition = hitInfo.point;
                }
            }
            else // Raycasat 가 실패하면 카메라의 정면으로 1000 m 떨어진 지점을 설정
            {

            }

            // aimingTargetPosition을 aimTarget의 위치로 설정
            aimTarget.position = aimingTargetPosition;
        }

        private void LateUpdate()
        {
            CameraRotation(); //카메라는 lateupdate 에 하는것이 좋음
        }

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (look.sqrMagnitude >= _threshold)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = 1.0f;

                cinemachineTargetYaw += look.x * deltaTimeMultiplier * cameraHorizontalSpeed;
                cinemachineTargetPitch += look.y * deltaTimeMultiplier * cameraVerticalSpeed;
            }

            // clamp our rotations so our values are limited 360 degrees
            cinemachineTargetYaw = ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);
            cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, bottomClamp, topClamp);

            // Cinemachine will follow this target
            cinemachineCameraTarget.transform.rotation = Quaternion.Euler(cinemachineTargetPitch + cameraAngleOverride, cinemachineTargetYaw, 0.0f);
        }

        private void Move()
        {
            if (!isEnableMovement)
                return;

            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = isSprint ? sprintSpeed : moveSpeed;

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (move == Vector2.zero) targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = move.magnitude;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * speedChangeRate);
                //Lerp 보관하는코드

                // round speed to 3 decimal places
                speed = Mathf.Round(speed * 1000f) / 1000f;
            }
            else
            {
                speed = targetSpeed;
            }

            animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * speedChangeRate);
            if (animationBlend < 0.01f) animationBlend = 0f;

            // normalise input direction
            Vector3 inputDirection = new Vector3(move.x, 0.0f, move.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (move != Vector2.zero)
            {
                targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity,
                    rotationSmoothTime);

                if (!isStrafe)
                {
                    // rotate to face input direction relative to camera position
                    transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                }
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

            // move the player
            controller.Move(targetDirection.normalized * (speed * Time.deltaTime) +
                             new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

      /*  public void OnTakenAnimationEvent(string eventName)
        {
            if (eventName.Equals("Reload"))
            {
                isReloading = false;
                animator.SetLayerWeight(1, 0f); //재장전을 끝내고 layerweight 를 다시 0 으로 만들어서 원래 애니메이션으로 변경하는듯
            }
        }*/
    }
}
