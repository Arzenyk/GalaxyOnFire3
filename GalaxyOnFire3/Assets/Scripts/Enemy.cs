using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject escudo;
    public PlayerShield playerShield; // Referencia al script PlayerShield
   
    public GameObject explosionPrefab;
    public GameObject enemy;

    public float health = 3f;
    public float moveSpeed = 5f;
    public float stopDistance = 30f;                 // Distancia mínima para dejar de avanzar
    public GameObject projectilePrefab;
    public float fireRate = 2f;                      // Dispara cada 2 segundos
    public float projectileSpeed = 20f;
    public Transform firePoint;                      // Punto desde donde dispara

    private Transform target;
    private float fireCooldown;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerShield = escudo.GetComponent<PlayerShield>();
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            // Si está lejos, se acerca al jugador
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        // Disparo automático con cooldown
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate;
        }

        // Mirar hacia el jugador
        Vector3 lookDir = target.position - transform.position;
        lookDir.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDir);
    }

    void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * projectileSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            EnemyTakeDamage(1);
            // Destruir el proyectil
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Escudo") && playerShield.escudoActive)
        {
            enemy.GetComponent<Enemy>().EnemyTakeDamage(10f);
            Debug.Log("Golpe con escudo");
        }

    }

    public void EnemyTakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
