using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Gloves : MonoBehaviour
{
    public Animator handAnimator;
    public bool isLeftHand = true;
    private InputDevice handDevice;
    
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        if (isLeftHand)
        {
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, devices);
        }
        else
        {
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, devices);
        }

        if (devices.Count > 0)
        {
            handDevice = devices[0];
        }
    }
    void Update()
    {
        float gripValue = 0;
        float triggerValue = 0;
        if (handDevice.isValid)
        {
            handDevice.TryGetFeatureValue(CommonUsages.grip, out gripValue);
            handDevice.TryGetFeatureValue(CommonUsages.trigger, out triggerValue);
            handAnimator.SetFloat("GripAmount", (gripValue + triggerValue)/2);
        }
    }
}
