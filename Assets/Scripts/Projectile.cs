using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Projectile speed
    private readonly float projectileSpeed = 10;

    public ParticleSystem hitParticles;
    public ParticleSystem blowParticles;
    public string origin;

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
        switch (origin)
        {
            case "Player":
                if (collision.gameObject.CompareTag("Player"))
                {
                    return;
                }
                break;
            case "Enemy":
                if (collision.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Le joeur recois des degats");
                    PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
                    playerController.health--;
                }
                break;
            default:
                break;
        }

        PlayParticles(collision);
        Destroy(gameObject);
    }

    /// <summary>
    /// Play particles according to the object collided
    /// </summary>
    /// <param name="collision"></param>
    private void PlayParticles(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(hitParticles, transform.position, transform.rotation);
            hitParticles.Play();
            return;
        }

        Instantiate(blowParticles, transform.position, transform.rotation);
        blowParticles.Play();
    }
}
