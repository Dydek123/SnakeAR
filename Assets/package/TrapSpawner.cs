using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrapSpawner : MonoBehaviour
{
    [SerializeField] DrivingSurfaceManager DrivingSurfaceManager;
    [SerializeField] GameObject TrapPrefab;
    private TrapBehaviour Trap;

    public static Vector3 RandomInTriangle(Vector3 v1, Vector3 v2)
    {
        float u = Random.Range(0.0f, 1.0f);
        float v = Random.Range(0.0f, 1.0f);
        if (v + u > 1)
        {
            v = 1 - v;
            u = 1 - u;
        }

        return (v1 * u) + (v2 * v);
    }

    public static Vector3 FindRandomLocation(ARPlane plane)
    {
        var mesh = plane.GetComponent<ARPlaneMeshVisualizer>().mesh;
        var triangles = mesh.triangles;
        var triangle = triangles[(int)Random.Range(0, triangles.Length - 1)] / 3 * 3;
        var vertices = mesh.vertices;
        var randomInTriangle = RandomInTriangle(vertices[triangle], vertices[triangle + 1]);
        var randomPoint = plane.transform.TransformPoint(randomInTriangle);

        return randomPoint;
    }

    public void SpawnTrap(ARPlane plane)
    {
        var trapClone = GameObject.Instantiate(TrapPrefab);
        trapClone.transform.position = FindRandomLocation(plane);

        Trap = trapClone.GetComponent<TrapBehaviour>();
    }

    private void Update()
    {
        var lockedPlane = DrivingSurfaceManager.LockedPlane;
        if (lockedPlane != null)
        {
            if (Trap == null)
            {
                SpawnTrap(lockedPlane);
            }

            var trapPosition = Trap.gameObject.transform.position;
            trapPosition.Set(trapPosition.x, lockedPlane.center.y, trapPosition.z);
        }
    }
}