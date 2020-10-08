using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    private Rigidbody playerRb;

    private GameObject focalPoint;
    public GameObject powerupIndicator;

    [SerializeField] float speed;
    [SerializeField] float powerupStrength = 15.0f;

    public bool hasPowerup = false;

    void Start()
    {
        // reference to the game manager script
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // reference to the player's rigidbody component
        playerRb = GetComponent<Rigidbody>();
        // reference to the focal point of the player
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        MovePlayer();

        // if the player falls off the platform, the game is over
        if (transform.position.y < -15 && !gameManager.gameOver)
        {
            gameManager.GameOver();
        }
    }


    // moves player with the up and down arrow keys
    void MovePlayer()
    {
        float forwardInput = Input.GetAxis("Vertical");
        Vector3 offset = new Vector3(0, -0.5f, 0);

        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        // attaches powerup indicator to player's location
        powerupIndicator.transform.position = transform.position + offset;
    }

    // if a player runs into a powerup, make the powerup indicator appear for 7 seconds
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
            Destroy(other.gameObject);
        }
    }

    // if a player collides with an enemy while using a powerup, apply extra force to send the enemy flying
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    // powerup timer
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
}
