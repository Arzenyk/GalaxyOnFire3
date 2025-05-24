using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;  // Prefab del proyectil
    public Transform shootPoint;         // Lugar desde donde se dispara
    public float shootForce = 20f;       // Fuerza del disparo
    private bool controlsLocked = false;
    public PlayerMovement playerMovement;

    void Start()
    {
        // Si no se asign� un punto de disparo, usar el centro de la nave
        if (shootPoint == null)
            shootPoint = transform;
    }

    // Este m�todo se llama desde el bot�n de disparo
    public void Fire()
    {
        if (controlsLocked)
            return;

        // Crear el proyectil en la posici�n y rotaci�n del punto de disparo
        GameObject newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        // Obtener el Rigidbody y aplicar una fuerza hacia adelante
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        rb.velocity = shootPoint.forward * shootForce;

        // Destruir el proyectil autom�ticamente despu�s de 5 segundos
        Destroy(newProjectile, 10f);
    }

    public void LockControls()
    {
        controlsLocked = true;
    }
}
