using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UST
{
    public class RestartButton : MonoBehaviour
    {
        public void onClickRestartBtn()
        {
            Debug.Log("Button is Clicked");
            SceneManager.LoadScene("GameStageTest");
        }
    }
}
