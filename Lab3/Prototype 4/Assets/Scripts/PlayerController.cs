using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 150f;

    public bool hasPowerup;
    public bool powerupActive;

    public GameObject powerupIndicator;

    private InputSystem_Actions controls;
    private Rigidbody playerRb;
    private GameObject focalPoint;

    private float powerupStrength = 15.0f;

    public event System.Action OnFire;

    void Awake()
    {
        controls = new InputSystem_Actions();

        playerRb = GetComponent<Rigidbody>();

        focalPoint = GameObject.Find("Focal Point");

        controls.Player.Fire.performed += FirePerformed;

        OnFire += ActivatePowerup;
    }

    void OnEnable()
    {
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    void OnDestroy()
    {
        controls.Player.Fire.performed -= FirePerformed;
        OnFire -= ActivatePowerup;
    }

    void FixedUpdate()
    {
        Vector2 moveInput =
            controls.Player.Move.ReadValue<Vector2>();

        float forwardInput = moveInput.y;
        float horizontalInput = moveInput.x;

        Vector3 movement =
            focalPoint.transform.forward * forwardInput +
            focalPoint.transform.right * horizontalInput;

        playerRb.AddForce(movement * playerSpeed);
    }

    void Update()
    {
        if (powerupIndicator != null)
        {
            powerupIndicator.transform.position =
                transform.position + new Vector3(0, -0.5f, 0);
        }
    }

    private void FirePerformed(InputAction.CallbackContext context)
    {
        OnFire?.Invoke();
    }

    private void ActivatePowerup()
    {
        if (hasPowerup && !powerupActive)
        {
            powerupActive = true;

            Debug.Log("Powerup activated!");

            if (powerupIndicator != null)
            {
                powerupIndicator.SetActive(true);
            }

            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;

            Debug.Log("Powerup collected! Press Fire to activate.");

            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") &&
            powerupActive)
        {
            Rigidbody enemyRigidbody =
                collision.gameObject.GetComponent<Rigidbody>();

            Vector3 awayFromPlayer =
                collision.gameObject.transform.position -
                transform.position;

            Debug.Log(
                "Collided with " +
                collision.gameObject.name +
                " with powerup active"
            );

            enemyRigidbody.AddForce(
                awayFromPlayer * powerupStrength,
                ForceMode.Impulse
            );
        }
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);

        powerupActive = false;
        hasPowerup = false;

        if (powerupIndicator != null)
        {
            powerupIndicator.SetActive(false);
        }

        Debug.Log("Powerup expired!");
    }
}