using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class camMove : MonoBehaviour
    {
        public float mouseSensitivity = 100f;
        public Transform playerBody;
        private float xRotation = 0f;
        bool canFreeMouse = false;
              

        // Update is called once per frame
        void Update()
        {
            if(canFreeMouse == false)
            {
                Cursor.lockState = CursorLockMode.Locked; 

            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }

            if(Input.GetKey(KeyCode.Escape))
            {
                canFreeMouse = true;
            }
            else
            {
                canFreeMouse = false;
            }


            float MouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float MouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            xRotation -= MouseY;
            xRotation = Mathf.Clamp(xRotation, -89f, 89f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * MouseX);

        }
    }
}
