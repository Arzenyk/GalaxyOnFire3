using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float normalZ = -10f; // Distancia base
    public float boostedZ = -15f; // Distancia al acelerar
    public float smoothSpeed = 5f; // Velocidad de interpolación
    public ParticleSystem particulasVelocidad;

    private float targetZ;

    // FOV transition variables
    public float normalFOV = 60f;
    public float boostedFOV = 90f;
    public float fovSmoothSpeed = 5f;
    private float targetFOV;

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

    public void SetFOV(bool boosting)
    {
        targetFOV = boosting ? boostedFOV : normalFOV;
    }

    void Start()
    {
        targetZ = normalZ;
        targetFOV = normalFOV;
        if (Camera.main != null)
            Camera.main.fieldOfView = normalFOV;
    }

    void LateUpdate()
    {
        Vector3 localPos = transform.localPosition;
        localPos.z = Mathf.Lerp(localPos.z, targetZ, Time.deltaTime * smoothSpeed);
        transform.localPosition = localPos;

        // Smoothly interpolate FOV
        if (Camera.main != null)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFOV, Time.deltaTime * fovSmoothSpeed);
        }
    }
}
