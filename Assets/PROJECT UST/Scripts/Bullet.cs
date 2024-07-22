using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class Bullet : MonoBehaviour
    {
        public int decreaseHp = 1;

        public void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Hit! " + collision.gameObject.name);

            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.TryGetComponent(out TestController controller))
                {
                    controller.TakeDamage(decreaseHp);
                }
            }
        }
    }
}
