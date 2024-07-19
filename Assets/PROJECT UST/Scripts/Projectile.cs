using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class Projectile : MonoBehaviour
    {
        public float speed = 30f;
        public float lifeTime = 10f;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
