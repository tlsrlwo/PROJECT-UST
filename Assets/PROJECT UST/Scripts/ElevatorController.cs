using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class ElevatorController : MonoBehaviour
    {
        public bool isPlayerInside;
        public Vector3 topPosition;
        public Vector3 bottomPosition;

        [SerializeField] private GameObject elevator;

        private void Update()
        {
            ElevatorSystem();
        }

        private void OnTriggerEnter(Collider col)
        {
            if(col.CompareTag("Player"))
            {
                Debug.Log("Player is on the elevator");
                isPlayerInside = true;
            }
        }

        private void OnTriggerExit(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                Debug.Log("Player left the elevator");
                isPlayerInside = false;
            }
        }

        public void ElevatorSystem()
        {
            if(isPlayerInside)
            {
                if(elevator.transform.localPosition.y > bottomPosition.y)
                {
                    elevator.transform.Translate(elevator.transform.up * Time.deltaTime * -5);
                }
            }
            if(!isPlayerInside)
            {
                if (elevator.transform.position.y < topPosition.y)
                {
                    elevator.transform.Translate(elevator.transform.up * Time.deltaTime * 3);
                }
            }
        }
    }
}
