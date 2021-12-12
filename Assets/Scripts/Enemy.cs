using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ParticleSystem deadParticles;
    public GameObject projectilePrefab;
    public int life;
    public int speed;
    public bool dropItemp = false;

    private Rigidbody enemyRb;
    private new CapsuleCollider collider;
    private Animator animator;
    private GameObject player;
    private GameManager gameManager;

    private bool particlesSent = false;
    private bool isBoss = false;
    private float timeParticles;
    private float timeDestroy;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.Find("Player");

        isBoss = life >= 20;
        timeParticles = isBoss ? 2 : 2.5f;
        timeDestroy = timeParticles == 2 ? 4 : 3.5f;

        if (projectilePrefab != null)
        {
            StartCoroutine("EnemyShooting");
        }
    }

    IEnumerator EnemyShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            if(IsTargetVisible(GameObject.Find("Main Camera").GetComponent<Camera>(), gameObject))
            {
                Instantiate(projectilePrefab, transform.position + (transform.forward * 1.3f) + transform.up, transform.rotation);
            }
        }
    }

    void Update()
    {
        if (life <= 0)
        {
            animator.Play("Defeat");
            enemyRb.detectCollisions = false;
            Destroy(collider);
            particlesSent = true;
            
            // Launch once Coroutine
            if (isBoss)
            {
                StartCoroutine(gameManager.Win());
            }
            Destroy(gameObject, timeDestroy);
            return;
        }

        if (IsTargetVisible(GameObject.Find("Main Camera").GetComponent<Camera>(), gameObject) && life >= 0)
        {
            Vector3 lookAtPos = player.transform.position;
            lookAtPos.y = transform.position.y;
            gameObject.transform.LookAt(lookAtPos);

            animator.SetBool("Run", true);
            gameObject.transform.position += (transform.forward * speed/10 * Time.deltaTime);
        }
        else
        {
            // Make the enemy immobile
            enemyRb.velocity = Vector3.zero;
            animator.SetBool("Run", false);
        }
    }

    private void OnDestroy()
    {
        if (particlesSent)
        {
            Instantiate(deadParticles, transform.position, transform.rotation);
            deadParticles.Play();

            if (dropItemp)
            {
                ListDrops listDrops;
                listDrops = GameObject.FindGameObjectWithTag("ListDrops").GetComponent<ListDrops>();
                Instantiate(listDrops.dropList[0], transform.position, transform.rotation);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            life--;
        }

        if (collision.collider.CompareTag("Player"))
        {
            animator.Play("Attack");
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