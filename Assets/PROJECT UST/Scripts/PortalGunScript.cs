using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class PortalGunScript : MonoBehaviour
    {
        [SerializeField] private CrosshairUI crosshairUI;
        [SerializeField] private Transform plyCamTransform;
        [SerializeField] private LayerMask portalWallLayer;

        public GameObject portalA;
        public GameObject portalB;


        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                float rayHitDistance = 50f;
                if (Physics.Raycast(plyCamTransform.position, plyCamTransform.forward, out RaycastHit raycastHit, rayHitDistance, portalWallLayer))
                {
                    //Debug.Log("It's the right wall");
                    Vector3 hitPos = raycastHit.point;

                    portalA.transform.position = hitPos;

                    GameObject hit = raycastHit.transform.gameObject;

                    //A.transform.rotation = hit.transform.rotation;
                    portalA.transform.forward = -raycastHit.normal;
                    portalA.SetActive(true);

                    crosshairUI.SetActiveBlueCrosshair(false);
                }
                else
                {
                    crosshairUI.SetActiveBlueCrosshair(true);
                    portalA.SetActive(false);
                }
            }
            if (Input.GetButtonDown("Fire2"))
            {
                float rayHitDistance = 50f;
                if (Physics.Raycast(plyCamTransform.position, plyCamTransform.forward, out RaycastHit raycastHit, rayHitDistance, portalWallLayer))
                {
                    //Debug.Log("It's the right wall");
                    Vector3 hitPos = raycastHit.point;

                    portalB.transform.position = hitPos;

                    GameObject hit = raycastHit.transform.gameObject;

                    //A.transform.rotation = hit.transform.rotation;
                    portalB.transform.forward = raycastHit.normal;
                    portalB.SetActive(true);

                    crosshairUI.SetActiveOrangeCrosshair(false);
                }
                else
                {
                    crosshairUI.SetActiveOrangeCrosshair(true);
                    portalB.SetActive(false);
                }
            }
        }
    }
}
