using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class PressE : MonoBehaviour
    {
        public GameObject explanationCanvas;
              
        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                explanationCanvas.SetActive(true);
            }
        }
        private void OnTriggerExit(Collider col)
        {
            if(col.CompareTag("Player"))
            {
                explanationCanvas.SetActive(false);
                Destroy(explanationCanvas);
            }
        }
    }
}
