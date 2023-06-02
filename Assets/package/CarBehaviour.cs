using System.Collections;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    private float Speed = 1.0f;
    private bool GameOver = false;
    private UIManager UiManager;
    private ParticleSystem PackageParticle;
    private ParticleSystem StoneParticle;
    private ReticleBehaviour _reticle;
    public ReticleBehaviour Reticle { get => _reticle; set => _reticle = value; }

    private void Start()
    {
        UiManager = GetComponent<UIManager>();
    }

    private void Update()
    {
        var trackingPosition = Reticle.transform.position;
        if (Vector3.Distance(trackingPosition, transform.position) < 0.1)
        {
            return;
        }

        var lookRotation = Quaternion.LookRotation(trackingPosition - transform.position);
        transform.rotation =
            Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        transform.position =
            Vector3.MoveTowards(transform.position, trackingPosition, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var Package = other.GetComponent<PackageBehaviour>();
        var TrapBehaviour = other.GetComponent<TrapBehaviour>();
        if (Package != null)
        {
            Speed += 0.2f;
            var particles = Instantiate(PackageParticle, transform.position, Quaternion.Euler(Vector3.up));
            Destroy(particles.gameObject, 1f);
            Destroy(other.gameObject);
        }
        if (TrapBehaviour != null)
        {
            GameOver = true;
            var particles = Instantiate(StoneParticle, transform.position, Quaternion.Euler(Vector3.up));
            Destroy(particles.gameObject, 5f);
            Destroy(other.gameObject);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }

    public bool GetGameOver()
    {
        return GameOver;
    }

    public void SetGameOver(bool gameOver)
    {
        GameOver = gameOver;
    }

    public void SetPackageParticle(ParticleSystem particle)
    {
        PackageParticle = particle;
    }

    public void SetStoneParticle(ParticleSystem particle)
    {
        StoneParticle = particle;
    }
}
