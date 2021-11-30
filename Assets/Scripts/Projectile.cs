using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Projectile speed
    private readonly float projectileSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Projectiles always go straight forward
        transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
    }

    // Destroy when hits objects
    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Player")) Destroy(gameObject);
    }
}
