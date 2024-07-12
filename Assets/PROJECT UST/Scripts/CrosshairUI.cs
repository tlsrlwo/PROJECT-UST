using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class CrosshairUI : MonoBehaviour
    {
        [SerializeField] private GameObject orangeCrosshair;
        [SerializeField] private GameObject blueCrosshair;

        public void SetActiveOrangeCrosshair(bool isActive)
        {
            orangeCrosshair.SetActive(isActive);
        }

        public void SetActiveBlueCrosshair(bool isActive)
        {
            blueCrosshair.SetActive(isActive);
        }
    }
}
