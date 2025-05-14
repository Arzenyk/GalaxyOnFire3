using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnRate = 2f;               // Cada cu�ntos segundos aparece uno
    public float spawnDistance = 50f;          // Distancia al frente de la nave
    public float spawnRange = 20f;             // Variaci�n lateral y vertical

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Asegurate de que tu nave tenga este tag
        InvokeRepeating(nameof(SpawnAsteroid), 1f, spawnRate);
    }

    void SpawnAsteroid()
    {
        if (player == null) return;

        // Posici�n aleatoria frente al jugador
        Vector3 spawnPos = player.position + player.forward * spawnDistance;
        spawnPos += new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0f);

        // Rotaci�n aleatoria para que vayan en distintas direcciones
        Quaternion randomRot = Quaternion.Euler(0, Random.Range(0, 360), 0);

        Instantiate(asteroidPrefab, spawnPos, randomRot);
    }
}

