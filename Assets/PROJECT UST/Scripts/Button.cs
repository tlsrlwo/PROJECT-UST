using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class Button : MonoBehaviour
    {
        public Animator animator;
        private bool clicked;

        [SerializeField] private GameObject door;
        private bool isOpened;
        //[SerializeField] private Animation doorAnim;

        private List<Transform> pressedObjects = new List<Transform>();

        private void Start()
        {
            animator.SetBool("isClicked", false);
           
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PickUps") || other.CompareTag("Player"))
            {
                //Debug.Log("Button Clicked");
                pressedObjects.Add(other.transform.root);
                animator.SetBool("isClicked", true);

                clicked = true;
                if(!isOpened)
                {
                    isOpened = true;
                    door.transform.position += new Vector3(0, 4, 0);
                }
               
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

                    if(isOpened)
                    {
                        door.transform.position -= new Vector3(0, 4, 0);
                        isOpened = false;
                    }
                    
                }
            }
        }


    }
}
