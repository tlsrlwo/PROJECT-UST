using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class AnimatorSync : MonoBehaviour
    {
        private Animator animator;
        private PlayerController playerController;

        // Start is called before the first frame update
        private void Awake()
        {
            animator = GetComponent<Animator>();
            playerController = GetComponentInParent<PlayerController>();   
            
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
