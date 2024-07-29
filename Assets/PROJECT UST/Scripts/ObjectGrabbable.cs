using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class ObjectGrabbable : MonoBehaviour
    {
        private Collider objectCollider;
        private Rigidbody objectsRb;
        private Transform objectGrabPointTransform;
        private bool isGrabbed = false;

        private void Awake()
        {
            objectCollider = GetComponent<Collider>();
            objectsRb = GetComponent<Rigidbody>(); 
        }

        public void Grab(Transform objectGrabPointTransform)
        {
            this.objectGrabPointTransform = objectGrabPointTransform;
            objectsRb.useGravity = false;
            isGrabbed = true;
            objectCollider.isTrigger = true;
        }

        public void Drop()
        {
            this.objectGrabPointTransform = null;
            objectsRb.useGravity = true;
            isGrabbed = false;
            objectCollider.isTrigger = false;
        }

        private void Update()
        {
            if (isGrabbed)
            {
                objectsRb.transform.forward = TestController.Instance.transform.forward;            
            }
        }

        private void FixedUpdate()
        {
            if(objectGrabPointTransform != null)
            {
                float lerpSpeed = 10f;
                Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
                objectsRb.MovePosition(newPosition);
                //objectsRb.angularVelocity = new Vector3(1, 2, 3);
            }
        }
    }
}
