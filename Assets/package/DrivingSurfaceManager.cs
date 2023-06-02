using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DrivingSurfaceManager : MonoBehaviour
{
    private ARPlaneManager _planeManager;
    public ARPlaneManager PlaneManager { get => _planeManager; set => _planeManager = value; }
    private ARRaycastManager _raycastManager;
    public ARRaycastManager RaycastManager { get => _raycastManager; set => _raycastManager = value; }
    private ARPlane _lockedPlane;
    public ARPlane LockedPlane { get => _lockedPlane; set => _lockedPlane = value; }

    public void LockPlane(ARPlane keepPlane)
    {
        var arPlane = keepPlane.GetComponent<ARPlane>();
        foreach (var plane in PlaneManager.trackables)
        {
            if (plane != arPlane)
            {
                plane.gameObject.SetActive(false);
            }
        }

        LockedPlane = arPlane;
        PlaneManager.planesChanged += DisableNewPlanes;
    }

    private void Start()
    {
        PlaneManager = GetComponent<ARPlaneManager>();
        RaycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (LockedPlane?.subsumedBy != null)
        {
            LockedPlane = LockedPlane.subsumedBy;
        }
    }

    private void DisableNewPlanes(ARPlanesChangedEventArgs args)
    {
        foreach (var plane in args.added)
        {
            plane.gameObject.SetActive(false);
        }
    }
}
