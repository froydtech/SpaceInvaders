using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Zenva.VR
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Grabbable : MonoBehaviour
    {
        //options when releasing
        public enum ReleaseAction { nothing, backToOrigin, throws }

        [Tooltip("Does it face forward when picking it up")]
        public bool facesForward;

        [Tooltip("What happens when it is released")]
        public ReleaseAction releaseAction;

        [Tooltip("Event that triggers when it is grabbed")]
        public UnityEvent onGrab;

        [Tooltip("Event triggered when released")]
        public UnityEvent onRelease;


        GrabController grabCtrl;

        [Tooltip("If it resets position, we need to remember the position")]
        Vector3 originalPosition;

        [Tooltip("If it resets rotation, we'll want to remember original rotation")]
        Quaternion originalRotation;

        [Tooltip("When we drop it, we no longer want it to be a child of our hand in the scene")]
        Transform originalParent;

       
        Rigidbody rb;

        [Tooltip("If it's kinematic, we'll want to remember that")]
        bool isKinematic;
     
        Vector3 ctrlVelocity;
        Vector3 prevPosition;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            isKinematic = rb.isKinematic;

            originalParent = transform.parent;

        }

        void FixedUpdate ()
        {
            if(grabCtrl && releaseAction == ReleaseAction.throws)
            {
                ctrlVelocity = (transform.position - prevPosition) / Time.fixedDeltaTime;
                prevPosition = transform.position;
            }

        }

        public void Grab(GrabController grabCtrl)
        {
            //if the controller already has grabbed the object, then return
            if (this.grabCtrl)
            {
                return;
            }

            //otherwise, object is picked up
            this.grabCtrl = grabCtrl;

            //objects original position and rotation is stored, and the previous position (if we want it reset) is stored
            originalPosition = transform.position;
            originalRotation = transform.rotation;
            prevPosition = originalPosition;

            //if the object has to face forward when picked up, then the grabbed object is transformed to forward.
            if (facesForward)
            {
                transform.forward = grabCtrl.transform.forward;
            }

            //we make the object being picked up a child of the controller that grabbed it
            transform.parent = grabCtrl.transform;

            //while it's picked up, it's loosed from the whims of physics and grabbity
            rb.isKinematic = true;

            onGrab.Invoke();
        }

        public void Release()
        {
            if (!grabCtrl)
            {
                return;
            }

            transform.parent = originalParent;
            rb.isKinematic = isKinematic;

            switch(releaseAction)
            {
                case ReleaseAction.backToOrigin:
                    BackToOrigin();
                    break;
                case ReleaseAction.throws:
                    ThrowItem();
                    break;
            }

            onRelease.Invoke();
            grabCtrl = null;
        }

        void BackToOrigin()
        {
            transform.position = originalPosition;
            transform.rotation = originalRotation;

        }

        void ThrowItem()
        {
            rb.isKinematic = false;
            rb.velocity = ctrlVelocity;
        }
    }
}

