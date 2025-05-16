using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public GameObject explosionPrefab; // Asignalo en el Inspector

    public float moveSpeed = 5f;

    void Update()
    {
        // Mueve el asteroide hacia adelante en su propia dirección
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            // Instanciamos la explosión en la posición actual
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Destruir este objeto y el proyectil
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
