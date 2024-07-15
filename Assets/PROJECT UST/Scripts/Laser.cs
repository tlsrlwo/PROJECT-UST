using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering;

namespace UST
{
    public class Laser : MonoBehaviour
    {
        /* private LineRenderer lr;
         [SerializeField] private GameObject laserPoint;

         private void Start()
         {
             lr = GetComponent<LineRenderer>();
         }

         private void Update()
         {
             RaycastHit hit;

             if(Physics.Raycast(laserPoint.transform.position, laserPoint.transform.forward, out hit))
             {
                 if(hit.collider)
                 {
                     lr.SetPosition(1, new Vector3(0, 0, hit.distance));
                 }
                 else
                 {
                     lr.SetPosition(1, new Vector3(0, 0, 5000));
                 }
             }
         }*/

        [SerializeField] private LineRenderer lr;
        [SerializeField] private float laserDisatnce = 8f;
        [SerializeField] private LayerMask ignoreMask;
        [SerializeField] private UnityEvent OnHitTarget;

        private RaycastHit rayHit;
        private Ray ray;

        private void Start()
        {
            lr.positionCount = 2;
        }
        private void Update()
        {
            ray = new(transform.position, transform.forward);

            if(Physics.Raycast(ray, out rayHit, laserDisatnce, ~ignoreMask))
            {
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, rayHit.point);
                if(rayHit.collider.TryGetComponent(out Target target))
                {
                    target.Hit();
                    OnHitTarget?.Invoke();
                }
            }
            else
            {
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, transform.position + transform.forward * laserDisatnce);
            }


        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, ray.direction * laserDisatnce);
        }

    }
}
