using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    GameManager gameManager;
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;

    void Start()
    {
        // reference to the game manager script
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        SpawnEnemy();
    }

    // spawns waves of enemies on the field, incrementing by 1 enemy every time a wave is eliminated
    void SpawnEnemy()
    {
        // get the current enemy count
        enemyCount = FindObjectsOfType<Enemy>().Length;

        // if all enemies on the field are eliminated, spawn another powerup and another enemy wave
        if (enemyCount == 0 && !gameManager.gameOver)
        {
            SpawnPowerup();
            SpawnEnemyWave(waveNumber);
            waveNumber++;
        }
    }

    // generate a random spawn position on the field
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    // spawn enemies in waves starting at 3 at random positions
    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    // spawn one powerup at a random position
    private void SpawnPowerup()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }
}
