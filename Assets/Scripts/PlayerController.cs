using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;

    // Player Movements
    private float horizontalInput;
    private float verticalInput;
    private readonly float movementSpeed = 5;

    // For the rotation while throwing projectiles
    private Vector3 lookingForward = new Vector3(0, 0, 0);
    private Vector3 lookingBackward = new Vector3(0, 180, 0);
    private Vector3 lookingRight = new Vector3(0, 90, 0);
    private Vector3 lookingLeft = new Vector3(0, -90, 0);

    // Camera Managing
    private float yPositionCamera;
    public GameObject cameraGameObject;
    private Vector3 positionCameraComparedPlane = new Vector3(0, 10, -2);
    private Vector3 desiredPosition, smoothPosition;

    // Projectiles Managing
    private float delay;
    private float delayThrow = .7f;
    public GameObject projectilePrefab;

    // Reach the end of the level boolean
    public bool win = false;
    public bool isBossRoomReached = false;

    private new Rigidbody rigidbody;
    private new CapsuleCollider collider;

    // Player's animation
    public Animator playerAnim;
    public int health;

    // Audio
    public AudioClip throwAudio;
    public AudioClip deathClip;

    // Start is called before the first frame update
    void Start()
    {
        // To keep the camera to the same height
        yPositionCamera = cameraGameObject.gameObject.transform.position.y;
        
        rigidbody = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();

        health = playerAnim.GetInteger("health");

        delay = Time.time;

        StartCoroutine(UnlockPosition());
    }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private IEnumerator UnlockPosition()
    {
        yield return new WaitForSeconds(3);
        rigidbody.velocity = new Vector3(0, 0, 0);
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        rigidbody.velocity = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health <= 0)
        {
            Destroy(collider);
            rigidbody.detectCollisions = false;
            return;
        }

        SetMoving();

        // Translate direction according to the rotation of the player
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * verticalInput, Space.World);
        transform.Translate(Vector3.right * Time.deltaTime * movementSpeed * horizontalInput, Space.World);

        // Throw a projectile controls
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(lookingForward); 
            ThrowProjectile();
        } else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(lookingLeft);
            ThrowProjectile();
        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(lookingBackward);
            ThrowProjectile();
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(lookingRight);
            ThrowProjectile();
        }

        ChangeCameraPosition();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Change position of the camera
        if (collision.gameObject.tag.Contains("Plane"))
        {
            positionCameraComparedPlane.x = collision.transform.position.x;
            positionCameraComparedPlane.z = collision.transform.position.z - 2;
            positionCameraComparedPlane.y = yPositionCamera;
            desiredPosition = positionCameraComparedPlane;

            if (collision.gameObject.tag.Contains("Boss")) ShowBossObjective();
        }

        // Lose one health
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Animation trigger
            playerAnim.SetInteger("health", playerAnim.GetInteger("health") - 1);
            // GameManager trigger
            health--;
            ChangeDisplayedHealth(health);
        }

        // Win one health
        if (collision.gameObject.CompareTag("Potion"))
        {
            // GameManager trigger
            if(health < 3)
            {
                health++;
                ChangeDisplayedHealth(health);
                //TODO Mettre le son ici !
            }
            Destroy(collision.gameObject);
        }
    }

    private void ChangeCameraPosition()
    {
        if (cameraGameObject.transform.position != desiredPosition)
        {
            smoothPosition = Vector3.Lerp(cameraGameObject.transform.position, desiredPosition, 2f * Time.deltaTime);
            cameraGameObject.transform.position = smoothPosition;

            if (Vector3.Distance(cameraGameObject.transform.position, desiredPosition) < 0.1f)
            {
                cameraGameObject.transform.position = desiredPosition;
            }
        }
    }

    /// <summary>
    /// Throw Projectiles in front of the player gameObject
    /// </summary>
    private void ThrowProjectile()
    {
        if (delay <= Time.time)
        {
            GetComponent<AudioSource>().PlayOneShot(throwAudio, .35f);
            playerAnim.SetTrigger("throw_trig");
            Instantiate(projectilePrefab, transform.position + (transform.forward * 1.3f) + transform.up, transform.rotation);
            delay = Time.time + delayThrow;
        }
    }

    private void SetMoving()
    {
        // Movement based on QASD keys
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        playerAnim.SetFloat("v", Math.Abs(verticalInput));
        playerAnim.SetFloat("h", Math.Abs(horizontalInput));
    }

    public void ChangeDisplayedHealth(int health)
    {
        // Change the health displayed
        switch (health)
        {
            case 3:
                gameManager.h1.enabled = true;
                gameManager.h2.enabled = true;
                gameManager.h3.enabled = true;
                break;
            case 2:
                gameManager.h1.enabled = true;
                gameManager.h2.enabled = true;
                gameManager.h3.enabled = false;
                break;
            case 1:
                gameManager.h1.enabled = true;
                gameManager.h2.enabled = false;
                gameManager.h3.enabled = false;
                break;
            default:
                gameManager.h1.enabled = false;
                gameManager.h2.enabled = false;
                gameManager.h3.enabled = false;
                StartCoroutine(GameOver());
                break;
        }
    }

    private IEnumerator GameOver()
    {
        GetComponent<AudioSource>().PlayOneShot(deathClip, .35f);
        yield return new WaitForSeconds(2);
        gameManager.GameOver();
    }

    private void ShowBossObjective()
    {
        StartCoroutine(gameManager.FadeInObjective(1f, gameManager.bossText));
        StartCoroutine(gameManager.FadeOutObjective(1f, gameManager.bossText));
    }
}
