using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 4f;
    public float spawnDistance = 60f;
    public float spawnSpread = 25f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        InvokeRepeating(nameof(SpawnEnemy), 2f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (player == null) return;

        // Generar una posición aleatoria frente al jugador
        Vector3 spawnPos = player.position + player.forward * spawnDistance;
        spawnPos += new Vector3(Random.Range(-spawnSpread, spawnSpread), Random.Range(-spawnSpread, spawnSpread), 0f);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
