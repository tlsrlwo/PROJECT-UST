using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class Button : MonoBehaviour
    {
        public Animator animator;
        private bool clicked;

        private List<Transform> pressedObjects = new List<Transform>();

        private void Start()
        {
            animator.SetBool("isClicked", false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PickUps") || other.CompareTag("Player"))
            {
                pressedObjects.Add(other.transform.root);
                animator.SetBool("isClicked", true);
                clicked = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("PickUps") || other.CompareTag("Player"))
            {
                pressedObjects.Remove(other.transform.root);

                if (pressedObjects.Count <= 0)
                {
                    clicked = false;
                    animator.SetBool("isClicked", false);
                }
            }
        }


    }
}
