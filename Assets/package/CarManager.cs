using System.Collections;

using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CarManager : MonoBehaviour
{
    [SerializeField] GameObject CarPrefab;
    [SerializeField] ReticleBehaviour Reticle;
    [SerializeField] DrivingSurfaceManager DrivingSurfaceManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] ParticleSystem PackageParticle;
    [SerializeField] ParticleSystem StoneParticle;
    private CarBehaviour Car;

    private void Update()
    {
        if (Car == null && WasTapped() && Reticle.CurrentPlane != null)
        {
            var obj = GameObject.Instantiate(CarPrefab);
            Car = obj.GetComponent<CarBehaviour>();
            Car.Reticle = Reticle;
            Car.SetPackageParticle(PackageParticle);
            Car.SetStoneParticle(StoneParticle);
            Car.transform.position = Reticle.transform.position;
            DrivingSurfaceManager.LockPlane(Reticle.CurrentPlane);
            uiManager.setGameStarted(true);
        }
        if (Car != null && Car.GetGameOver() == true)
        {
            uiManager.GameOver();
            Car.SetSpeed(0.0f);
        }

    }

    private bool WasTapped()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }

        if (Input.touchCount == 0)
        {
            return false;
        }

        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
        {
            return false;
        }

        return true;
    }

    public void setCarGameOver()
    {
        Car.SetGameOver(false);
        Car.SetSpeed(1.0f);
    }
}
