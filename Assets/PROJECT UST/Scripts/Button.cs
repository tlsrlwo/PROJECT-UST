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

        private bool isDoorActivate;
        public Vector3 openedPosition;
        public Vector3 closedPosition;

        //[SerializeField] private Animation doorAnim;

        private List<Transform> pressedObjects = new List<Transform>();

        private void Start()
        {
            animator.SetBool("isClicked", false);

        }

        private void Update()
        {
            UpdateDoor();
            //DoorOpen();
            //DoorClosed();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PickUps") || other.CompareTag("Player"))
            {
                //Debug.Log("Button Clicked");
                pressedObjects.Add(other.transform.root);
                animator.SetBool("isClicked", true);
                clicked = true;
                isDoorActivate = true;


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
                    isDoorActivate = false;

                    /* if(isOpened)
                     {
                         door.transform.position -= new Vector3(0, 4, 0);
                         isOpened = false;
                     }*/
                    //Invoke("DoorClosed", 1.0f);
                }
            }
        }

        public void UpdateDoor()
        {
            if (isDoorActivate)
            {
                // To do : Door Open
                if (door.transform.localPosition.y < openedPosition.y)
                {
                    door.transform.Translate(door.transform.up * Time.deltaTime * 5);
                }
            }
            else
            {
                // To do : Door Closed
                if (door.transform.localPosition.y > closedPosition.y)
                {
                    door.transform.Translate(-1 * door.transform.up * Time.deltaTime * 5);
                }
            }
        }


        //void DoorOpen()
        //{
        //    if (doorOpening == true)
        //    {
        //        door.transform.Translate(Vector3.up * Time.deltaTime * 5);
        //    }
        //    if (door.transform.position.y > 7f)
        //    {
        //        doorOpening = false;
        //    }
        //}

        //void DoorClosed()
        //{
        //    if (doorClosing == true)
        //    {
        //        door.transform.Translate(Vector3.down * Time.deltaTime * 5);
        //    }
        //    if (door.transform.position.y <= 2.5f)
        //    {
        //        doorClosing = false;
        //    }
        //}

    }
}
