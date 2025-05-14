using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Mueve el asteroide hacia adelante en su propia dirección
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Si choca con un proyectil, se destruye
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);  // Destruir el proyectil
            Destroy(gameObject);        // Destruir el asteroide
        }
    }
}
