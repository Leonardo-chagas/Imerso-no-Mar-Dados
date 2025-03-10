using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HMDInfoManager : MonoBehaviour
{
    public GameObject XRDeviceSimulator;
    public GameObject XROrigin;
    //public GameObject LocomotionSystem;
    //public GameObject XRRig;
    
    void Start()
    {
        Debug.Log("Is Device Active " + XRSettings.isDeviceActive);
        Debug.Log("Device Name is: " + XRSettings.loadedDeviceName);

        if(!XRSettings.isDeviceActive){
            Debug.Log("No Headset plugged");
        }
        else if(XRSettings.isDeviceActive && (XRSettings.loadedDeviceName == "Mock HMD"
        || XRSettings.loadedDeviceName == "MockHMD Display")){
            Debug.Log("Using Mock HMD");
            XRDeviceSimulator.SetActive(true);
            XROrigin.SetActive(true);
            //LocomotionSystem.SetActive(true);
        }
        else{
            Debug.Log("We Have a Headset " + XRSettings.loadedDeviceName);
            XRDeviceSimulator.SetActive(false);
            XROrigin.SetActive(true);
            //XRRig.SetActive(true);
        }
    }
}
