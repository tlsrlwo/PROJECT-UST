using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UST
{
    public class Target : MonoBehaviour
    {
        private void Update()
        {
            gameover();
        }

        public void Hit()
        {
            Debug.Log("Player Hit by Laser");
        }


        void gameover()
        {
            if(this.gameObject.transform.localPosition.y < 14)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
          
    }
}
