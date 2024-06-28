using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class ObjectGrabbable : MonoBehaviour
    {
        private Rigidbody objectsRb;
        private Transform objectGrabPointTransform;

        private void Awake()
        {
            objectsRb = GetComponent<Rigidbody>(); 
        }

        public void Grab(Transform objectGrabPointTransform)
        {
            this.objectGrabPointTransform = objectGrabPointTransform;
            objectsRb.useGravity = false;
        }
        public void Drop()
        {
            this.objectGrabPointTransform = null;
            objectsRb.useGravity = true;
        }    

        private void FixedUpdate()
        {
            if(objectGrabPointTransform != null)
            {
                float lerpSpeed = 10f;
                Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
                objectsRb.MovePosition(newPosition);
            }
        }
    }
}
