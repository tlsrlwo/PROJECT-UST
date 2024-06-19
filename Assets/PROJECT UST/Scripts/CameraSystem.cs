using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class CameraSystem : MonoBehaviour
    {
        public static CameraSystem Instance { get; private set; } = null;

        public bool IsTPSMode => isTPSMode;
        public float TargetFOV { get; set; } = 60.0f;

        public Cinemachine.CinemachineVirtualCamera tpsCamera;

        public float zoomSpeed = 5.0f;
        private bool isTPSMode = true;


        public void Awake()
        {
            Instance = this;
        }

        
        void Start()
        {
            isTPSMode = tpsCamera.gameObject.activeSelf;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        private void LateUpdate()
        {
            tpsCamera.m_Lens.FieldOfView = Mathf.Lerp(tpsCamera.m_Lens.FieldOfView, TargetFOV, zoomSpeed * Time.deltaTime);
            
        }
    }
}
