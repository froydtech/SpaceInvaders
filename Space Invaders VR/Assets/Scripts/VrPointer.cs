using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zenva.VR
{
    public class VrPointer : MonoBehaviour
    {
        [Tooltip("Maximum Interaction Distance")]
        public float maxDistance;

        [Tooltip("Shwo ray for debugging")]
        public bool showRay = false;

        //Keep track of interactables
        Interactable currInteractable;
        Interactable prevInteractable;

        public Vector3 EndPosition { get; private set; }

        // Update is called once per frame
        void Update()
        {
            RayCast();
        }

        void RayCast()
        {
            RaycastHit target;
            //found an object
            if (Physics.Raycast(transform.position, transform.forward, out target, maxDistance))
            {

                EndPosition = target.point;
                currInteractable = target.transform.GetComponent<Interactable>();

                //call selection method
                if (currInteractable)
                {
                    currInteractable.Over();
                }

            }

            //no object found
            else
            {                
                EndPosition = transform.position + transform.forward * maxDistance;
                //call unselection method
                if (currInteractable)
                {
                    currInteractable.Out();
                }
            }

            //check that selection chagned at all
            if (currInteractable != prevInteractable)
            {
                prevInteractable?.Out();
            }
            prevInteractable = currInteractable;

            if (showRay)
            {
                Debug.DrawRay(transform.position, EndPosition - transform.position, Color.blue);
            }
        }

        public void PressButton()
        {
            currInteractable?.ButtonDown();           
        }
    }
}
