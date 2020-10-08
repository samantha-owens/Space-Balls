using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody enemyRb;
    GameObject player;
    [SerializeField] float speed = 3.0f;

    void Start()
    {
        // reference to the game manager script
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        // reference to the enemy ball's rigidbody
        enemyRb = GetComponent<Rigidbody>();
        // reference to the player
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (!gameManager.gameOver)
        {
            MoveEnemy();
            DestroyEnemy();
        }
    }

    // moves enemy in direction of the player
    void MoveEnemy()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
    }

    // destroys enemy object if it falls off the platform, adds a point to the score for each enemy destroyed
    void DestroyEnemy()
    {
        if (transform.position.y < -15)
        {
            gameManager.AddScore();
            Destroy(gameObject);
        }
    }
}
