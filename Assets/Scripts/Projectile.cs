using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Projectile speed
    private readonly float projectileSpeed = 10;

    public ParticleSystem hitParticles;
    public ParticleSystem blowParticles;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Projectiles always go straight forward
        transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
        transform.Rotate(new Vector3(0, 0, 20));
    }

    // Destroy when hits objects
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        PlayParticles(collision);

        Destroy(gameObject);
    }

    private void PlayParticles(Collision collision)
    {
        ParticleSystem particles;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            particles = Instantiate(hitParticles, transform.position, transform.rotation);
            hitParticles.Play();
            return;
        }

        particles = Instantiate(blowParticles, transform.position, transform.rotation);
        blowParticles.Play();
    }
}
