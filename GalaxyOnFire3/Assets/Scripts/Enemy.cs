using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject explosionPrefab;

    public float health = 3f;
    public float moveSpeed = 5f;
    public float stopDistance = 30f;                 // Distancia m�nima para dejar de avanzar
    public GameObject projectilePrefab;
    public float fireRate = 2f;                      // Dispara cada 2 segundos
    public float projectileSpeed = 20f;
    public Transform firePoint;                      // Punto desde donde dispara

    private Transform target;
    private float fireCooldown;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            // Si est� lejos, se acerca al jugador
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        // Disparo autom�tico con cooldown
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
            // Instanciamos la explosi�n en la posici�n actual
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Destruir este objeto y el proyectil
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
