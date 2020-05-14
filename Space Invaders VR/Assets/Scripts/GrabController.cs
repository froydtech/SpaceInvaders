using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Zenva.VR
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class GrabController : MonoBehaviour
    {


        Grabbable item;
        bool isGripping;
        public void Grip()
        {
            isGripping = true;
        }

        public void Release()
        {
            isGripping = false;

            if(item)
            {
                item.Release();
            }
            
            item = null;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!item && isGripping && other.GetComponent<Grabbable>())
            {
                item = other.GetComponent<Grabbable>();
                item.Grab(this);
            }
        }
    }

}
