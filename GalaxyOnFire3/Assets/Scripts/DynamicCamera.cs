using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float normalZ = -10f; // Distancia base
    public float boostedZ = -15f; // Distancia al acelerar
    public float smoothSpeed = 5f; // Velocidad de interpolación
    public ParticleSystem particulasVelocidad;

    private float targetZ;

    // Esto lo llama el PlayerMovement cuando cambia el input
    public void SetBoost(bool boosting)
    {
        targetZ = boosting ? boostedZ : normalZ;

        if (boosting)
        {
            particulasVelocidad.gameObject.SetActive(true);
        }
        else
        {
            particulasVelocidad.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        targetZ = normalZ;
    }

    void LateUpdate()
    {
        Vector3 localPos = transform.localPosition;
        localPos.z = Mathf.Lerp(localPos.z, targetZ, Time.deltaTime * smoothSpeed);
        transform.localPosition = localPos;
    }
}
