using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZenvaVR
{
    public class VR2DButtonController : MonoBehaviour
    {
        //2d canvas button
        Button button;

        void Awake()
        {
            //get button
            button = GetComponentInChildren<Button>();
        }

        public void ButtonClick ()
        {
            button.onClick.Invoke();
        }

    }
}