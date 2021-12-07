using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

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
    private Vector3 positionCameraComparedPlane = new Vector3(0, 10, 0);
    private Vector3 desiredPosition, smoothPosition;

    // Projectiles Managing
    private float delay;
    private float delayThrow = .7f;
    public GameObject projectilePrefab;

    // Health
    public int health = 3;

    // Reach the end of the level boolean
    public bool win = false;

    private new Rigidbody rigidbody;

    // Player's animation
    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        // To keep the camera to the same height
        yPositionCamera = cameraGameObject.gameObject.transform.position.y;
        
        rigidbody = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();

        delay = Time.time;

        StartCoroutine(UnlockPosition());
    }

    IEnumerator UnlockPosition()
    {
        yield return new WaitForSeconds(3);
        rigidbody.velocity = new Vector3(0, 0, 0);
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        rigidbody.velocity = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
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

        if (cameraGameObject.transform.position != desiredPosition)
        {
            smoothPosition = Vector3.Lerp(cameraGameObject.transform.position, desiredPosition, 2f * Time.deltaTime);
            cameraGameObject.transform.position = smoothPosition;

            if (Vector3.Distance(cameraGameObject.transform.position, desiredPosition) < 0.1f)
            {
                //J'estime quil sont très très proche
                cameraGameObject.transform.position = desiredPosition;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Change position of the camera
        if (collision.gameObject.CompareTag("Plane"))
        {
            positionCameraComparedPlane.x = collision.transform.position.x;
            positionCameraComparedPlane.z = collision.transform.position.z;
            positionCameraComparedPlane.y = yPositionCamera;
            desiredPosition = positionCameraComparedPlane;
        }

        // Win the game
        if (collision.gameObject.CompareTag("Boss"))
        {
            win = true;
        }

        // Lose one health
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health--;
        }
    }

    /// <summary>
    /// Throw Projectiles in front of the player gameObject
    /// </summary>
    private void ThrowProjectile()
    {
        if (delay <= Time.time)
        {
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
        playerAnim.SetFloat("v", verticalInput);
        playerAnim.SetFloat("h", horizontalInput);
    }
}
