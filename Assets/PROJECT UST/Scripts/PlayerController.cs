using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float rotateSpeed = 5f;
        public float lastMouseX = 0;

        private Vector2 move;


        private bool isEnableMovement = true;

        // Update is called once per frame
        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            move = new Vector2(horizontal, vertical);

            bool isMouseLeftMoved = lastMouseX - Input.mousePosition.x < 0;
            bool isMouseMoved = Input.mousePosition.x != lastMouseX;
            if(isMouseMoved)
            {
                transform.Rotate(Vector3.up * rotateSpeed * (isMouseLeftMoved ? -1 : 1) * Time.deltaTime);
            }

        }

        private void Move()
        {
            
        }

        private void LateUpdate()
        {
            lastMouseX = Input.mousePosition.x;
        }
    }
}
