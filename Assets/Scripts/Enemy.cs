using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int life;
    public int speed;

    private Rigidbody enemyRb;
    private Animator animator;
    private GameObject player;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("Attack", true);
        }

        if (IsTargetVisible(GameObject.Find("Main Camera").GetComponent<Camera>(), gameObject))
        {
            Vector3 lookAtPos = player.transform.position;
            lookAtPos.y = transform.position.y;
            gameObject.transform.LookAt(lookAtPos);

            gameObject.transform.position += (transform.forward * speed/10 * Time.deltaTime);


        }
        else
        {
            // Make the enemy immobile
            enemyRb.velocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            life--;
        }
    }

    /// <summary>
    /// Check if a gameObject is visible by a camera
    /// </summary>
    /// <param name="camera">The camera</param>
    /// <param name="gameObject">The object</param>
    /// <returns></returns>
    private bool IsTargetVisible(Camera camera, GameObject gameObject)
    {
        // Get coordinates of 3D objects currently seen by the camera
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        Vector3 point = gameObject.transform.position;
        foreach (Plane plane in planes)
        {
            // If the player is not in the current plane anymore
            if (plane.GetDistanceToPoint(point) < 0)
            {
                return false;
            }
        }
        return true;
    }
}