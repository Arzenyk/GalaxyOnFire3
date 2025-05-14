using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 3f;              // Cantidad de vida
    public float moveSpeed = 5f;           // Velocidad de movimiento

    private Transform target;

    void Start()
    {
        // Buscamos la nave del jugador usando el tag "Player"
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target != null)
        {
            // Moverse hacia la nave
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Si choca con un proyectil, baja la vida
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject); // Destruye el proyectil
            TakeDamage(1f);            // Recibe 1 punto de daño
        }
    }

    void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Destroy(gameObject); // Destruir al enemigo
        }
    }
}
