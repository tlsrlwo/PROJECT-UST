using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class Button : MonoBehaviour
    {
        private Animator animator;
        private bool Clicked;

        private void Start()
        {
            animator = GetComponent<Animator>();
            animator.SetBool("isClicked", false);
        }

        private void OnTriggerStay(Collider other)
        {
            if(other.CompareTag("PickUps"))
            {
                animator.SetBool("isClicked", true);
                Clicked = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            animator.SetBool("isClicked", false);
            Clicked = false;
        }

        
    }
}
