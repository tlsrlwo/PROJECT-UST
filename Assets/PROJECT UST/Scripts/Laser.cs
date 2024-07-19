using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
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
        public bool isHit;

        private RaycastHit rayHit;
        private Ray ray;


        [Header("Shooting")]
        public float lastShootTime = 1.0f;
        public float fireRate = 0f;
        public float range = 100f;
        
        public Transform bulletSpawn;
        public GameObject bulletPrefab;
        public GameObject muzzlePrefab;

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
                    Invoke("Shoot", 0.5f);
                    //isHit = true;
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

        public void Shoot()
        {
            if (Time.time > lastShootTime + fireRate)
            {
                // Possible Shoot 

                lastShootTime = Time.time;

                // Create Muzzle
                var newMuzzle = Instantiate(muzzlePrefab);
                newMuzzle.transform.SetPositionAndRotation(bulletSpawn.position, bulletSpawn.rotation);
                newMuzzle.gameObject.SetActive(true);
                Destroy(newMuzzle, 1f);

                // Create Bullet
                var newBullet = Instantiate(bulletPrefab);
                newBullet.transform.SetPositionAndRotation(bulletSpawn.position, bulletSpawn.rotation);
                newBullet.gameObject.SetActive(true);

                // 두 오브젝트가 서로 충돌처리가 되지 않게 Unity Physics Engine에게 무시하도록 지시
                //Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
            }
            
        }

    }
}
