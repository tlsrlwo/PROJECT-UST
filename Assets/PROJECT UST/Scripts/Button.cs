using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

namespace UST
{
    public class Button : MonoBehaviour
    {
        public Animator animator;
        
        private bool clicked;

        [SerializeField] private GameObject door;
        private bool isOpened;
        public bool doorOpening;
        public bool doorClosing;

        //[SerializeField] private Animation doorAnim;

        private List<Transform> pressedObjects = new List<Transform>();

        private void Start()
        {
            animator.SetBool("isClicked", false);
           
        }

        private void Update()
        {
            DoorOpen();
            DoorClosed();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PickUps") || other.CompareTag("Player"))
            {
                //Debug.Log("Button Clicked");
                pressedObjects.Add(other.transform.root);
                animator.SetBool("isClicked", true);
                clicked = true;
                doorOpening = true;
                

                /*if(!isOpened)
                 {
                     isOpened = true;
                     door.transform.position += new Vector3(0, 4, 0);
                 }*/
                //Invoke("DoorOpen", 1.0f);
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
                    doorClosing = true;

                    /* if(isOpened)
                     {
                         door.transform.position -= new Vector3(0, 4, 0);
                         isOpened = false;
                     }*/
                    //Invoke("DoorClosed", 1.0f);
                }
            }
        }

        void DoorOpen()
        {
            if (doorOpening == true) 
            {
                door.transform.Translate(Vector3.up * Time.deltaTime * 5);              
            }
            if (door.transform.position.y > 7f)
            {
                doorOpening = false;
            }
        }

        void DoorClosed()
        {
           if(doorClosing == true)
            {
                door.transform.Translate(Vector3.down * Time.deltaTime * 5);
            }
            if (door.transform.position.y <= 2.5f)
            {
                doorClosing = false;
            }
        }

    }
}
