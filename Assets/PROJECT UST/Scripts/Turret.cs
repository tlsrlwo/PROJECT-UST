using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

namespace UST
{
    public class Turret : MonoBehaviour
    {
        public GameObject laserpoint;

        private void Update()
        {
            Fire();
        }
        void Fire()
        {
            RaycastHit hit;

            if (Physics.Raycast(laserpoint.transform.position, laserpoint.transform.forward, out hit))
            {
                if (hit.collider.tag == "Player")
                {
                    print("Hit : " + hit.collider.gameObject.name);
                }
            }


        }
    }

}
