using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class PortalTeleporter : MonoBehaviour
    {
        public Transform player;
        public Transform reciever;
        //public Collider teleportCollider;

        private bool playerIsOverlapping = false;

        // Update is called once per frame
        void Update()
        {
            if (playerIsOverlapping)
            {
                Vector3 portalToPlayer = player.position - transform.position;
                //float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
                float dotProduct = System.Math.Sign(Vector3.Dot(player.position - transform.position, transform.forward));

                // if this is true ; the player has moved across the portal
                if (dotProduct < 0f)
                {
                    //teleport
                    float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
                    rotationDiff += 180;
                    player.Rotate(Vector3.up, rotationDiff);

                    Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                    player.position = reciever.position + positionOffset;
                    playerIsOverlapping = false;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerIsOverlapping = true;
                Debug.Log("playerAttached");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerIsOverlapping = false;
                Debug.Log("PlayerisDetached");
            }
        }
    }
}
