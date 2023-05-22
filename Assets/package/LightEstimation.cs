using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(Light))]
public class LightEstimation : MonoBehaviour
{
    public ARCameraManager ARCameraManager;
    public Light Light;

    private void Start()
    {
        ARCameraManager.frameReceived += FrameReceived;

        Light = GetComponent<Light>();
    }

    private void FrameReceived(ARCameraFrameEventArgs args)
    {
        // TODO: Adjust lighting based on `args`.
    }
}
