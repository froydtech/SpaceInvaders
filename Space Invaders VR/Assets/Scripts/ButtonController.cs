using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System;
using UnityEngine.Events;



namespace Zenva.VR
{

    public class ButtonController : MonoBehaviour
    {



        //1 select a label from the unity inspector
        //2 find the ojbect that corresponds with the label
        //2 a. we need to get the label of the user selection
        //2 b. find the object in the dictionary
        //3 We need to use the object we found


        static readonly Dictionary<string, InputFeatureUsage<bool>> availableFeatures = new Dictionary<string, InputFeatureUsage<bool>>
        {
            { "triggerButton", CommonUsages.triggerButton },
            { "gripButton", CommonUsages.gripButton },
            { "thumbrest", CommonUsages.thumbrest },
            { "primary2DAxisClick", CommonUsages.primary2DAxisClick },
            { "primary2DAxisTouch", CommonUsages.primary2DAxisTouch },
            { "menuButton", CommonUsages.triggerButton },
            { "secondaryButton", CommonUsages.secondaryButton },
            { "secondaryTouch", CommonUsages.secondaryTouch },
            { "primaryButton", CommonUsages.primaryButton },
            { "primaryTouch", CommonUsages.primaryTouch },
        };
        public enum FeatureOptions
        {
            triggerButton,
            gripButton,
            thumbrest,
            primary2DAxisClick,
            primary2DAxisTouch,
            menuButton,
            secondaryButton,
            secondaryTouch,
            primaryButton
        };

        [Tooltip("Select an input feature")]
        public FeatureOptions feature;

        [Tooltip("Event when the button starts being pressed")]
        public UnityEvent OnPress;



        [Tooltip("Event when the button is released")]
        public UnityEvent OnRelease;

        //Pressing the button
        bool isPressed;


        //button press value
        bool inputValue;
        //selected feature object
        InputFeatureUsage<bool> selectedFeature;

        //keep devices that are detected
        List<InputDevice> devices;
        [Tooltip("Input Device Role (left/right hand)")]
        public InputDeviceRole deviceRole;



        void Awake()
        {
            devices = new List<InputDevice>();

            //get the label selected by the user

            string featureLabel = Enum.GetName(typeof(FeatureOptions), feature);



            //find the dictionary entry

            availableFeatures.TryGetValue(featureLabel, out selectedFeature);

        }
        // Update is called once per frame
        void Update()
        {
            //get the device we want to check 
            //whichever controller
            InputDevices.GetDevicesWithRole(deviceRole, devices);           

            // go through the devices
            for (int i=0; i <devices.Count; i++)
            {
                //check whether button is pressed
                //1. Check to see if we can read the state of the button
                //2. The buttons value should be true

                if (devices[i].TryGetFeatureValue(selectedFeature, out inputValue) && inputValue)
                { 
                       // check if already being pressed
                    if (!isPressed)
                    {
                        isPressed = true;

                        //trigger on press event
                        OnPress.Invoke();
                    }
                    
                }
                else if (isPressed)
                {
                    //update flag
                    isPressed = false;

                    //trigger onrelease event
                    OnRelease.Invoke();
                }
            }



        }




    }
}