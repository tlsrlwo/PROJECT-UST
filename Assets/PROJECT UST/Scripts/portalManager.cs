using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class portalManager : MonoBehaviour
    {
        public Transform APos;
        public Transform BPos;

        CharacterController controller;

        private void OnTriggerEnter(Collider col)
        {
            controller = GetComponent<CharacterController>();  

            if (col.CompareTag("Portal A"))
            {
                controller.enabled = false;
                transform.position = BPos.transform.position;
                transform.rotation = new Quaternion(transform.rotation.x, BPos.rotation.y, transform.rotation.z, transform.rotation.w);

                controller.enabled = true;
            }

            if (col.CompareTag("Portal B"))
            {
                controller.enabled = false;
                transform.position = APos.transform.position;
                transform.rotation = new Quaternion(transform.rotation.x, APos.rotation.y, transform.rotation.z, transform.rotation.w);

                controller.enabled = true;
            }
        }

    }
}
