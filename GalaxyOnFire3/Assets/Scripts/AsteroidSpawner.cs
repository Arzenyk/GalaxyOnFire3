using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public GameObject asteroidPrefab2;
    public float spawnRate = 2f;               // Cada cuántos segundos aparece uno
    public float spawnDistance = 50f;          // Distancia al frente de la nave
    public float spawnRange = 20f;             // Variación lateral y vertical

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        InvokeRepeating(nameof(SpawnAsteroid), 1f, spawnRate);
    }

    void SpawnAsteroid()
    {
        if (player == null) return;

        // Posición aleatoria frente al jugador
        Vector3 spawnPos = player.position + player.forward * spawnDistance;
        spawnPos += new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0f);

        // Rotación aleatoria para que vayan en distintas direcciones
        Quaternion randomRot = Quaternion.Euler(0, Random.Range(0, 360), 0);

        Instantiate(asteroidPrefab, spawnPos, randomRot);
        Instantiate(asteroidPrefab2, spawnPos, randomRot);
    }
}

