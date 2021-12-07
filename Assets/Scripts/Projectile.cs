using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Projectile speed
    private readonly float projectileSpeed = 10;
    private GameObject player;
    private Vector3 forwardPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
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
        if(!collision.gameObject.CompareTag("Player")) Destroy(gameObject);
    }
}
