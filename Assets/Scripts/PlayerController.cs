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
    private Vector3 positionCameraComparedPlane = new Vector3(0, 8, 0);

    // Projectiles Managing
    public bool throwableProjectiles = true;
    public GameObject projectilePrefab;

    // Health
    public int health = 3;

    // Reach the end of the level boolean
    public bool win = false;

    // Start is called before the first frame update
    void Start()
    {
        // To keep the camera to the same height
        yPositionCamera = cameraGameObject.gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement based on QASD keys
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

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
    }

    void OnCollisionEnter(Collision collision)
    {
        // Change position of the camera
        if (collision.gameObject.CompareTag("Plane"))
        {
            positionCameraComparedPlane.x = collision.transform.position.x;
            positionCameraComparedPlane.z = collision.transform.position.z;
            positionCameraComparedPlane.y = yPositionCamera;
            cameraGameObject.transform.position = positionCameraComparedPlane;
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
        if (throwableProjectiles)
        {
            Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation);
            throwableProjectiles = false;
        }
    }
}
