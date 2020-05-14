using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Zenva.VR { 
    public class Interactable : MonoBehaviour
    {
        public UnityEvent OnOver;
        public UnityEvent OnOut;
        public UnityEvent OnButtonDown;

        bool isOver = false;

        public void Over()
        {
            if (!isOver)
            {
                isOver = true;
                OnOver.Invoke();
            }
        
        }

        public void Out()
        {
            // check we were selecting it
            if (isOver)
            {
                //update the flag
                isOver = false;

                //trigger event
                OnOut.Invoke();            
            }
        }

        public void ButtonDown()
        {
            OnButtonDown.Invoke();
        }
    }
}
