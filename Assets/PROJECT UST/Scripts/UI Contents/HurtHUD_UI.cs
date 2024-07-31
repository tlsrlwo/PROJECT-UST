using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UST
{
    public class HurtHUD_UI : MonoBehaviour
    {
        public CanvasGroup canvasGroup;

        public void SetAlpha(float value)
        {
            canvasGroup.alpha = value;
        }
    }
}
