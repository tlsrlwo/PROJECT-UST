using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class Bullet : MonoBehaviour
    {
        public int decreaseHp = 1;
        public float speed = 30f;
        public float lifeTime = 10f;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            transform.Translate(transform.forward * speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.TryGetComponent(out TestController controller))
                {
                    Debug.Log("PlayerHitByBullet");
                    controller.TakeDamage(decreaseHp);
                }
            }
        }

        //public void OnCollisionEnter(Collision collision)
        //{
        //    Debug.Log("Hit! " + collision.gameObject.name);

        //    if (collision.gameObject.CompareTag("Player"))
        //    {
        //        if (collision.gameObject.TryGetComponent(out TestController controller))
        //        {
        //            controller.TakeDamage(decreaseHp);
        //        }
        //    }
        //}
    }
}
