using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 10f;
    public float verticalSpeed = 5f;
    public float turnSpeed = 50f;

    // Suavizado de rotaci�n
    public float rotationSmoothness = 5f;

    // �ngulos m�ximos de inclinaci�n
    public float maxTiltAngle = 30f;      // Inclinaci�n lateral (roll)
    public float maxPitchAngle = 15f;     // Inclinaci�n nariz arriba/abajo

    private float forwardInput = 0f;
    private float verticalInput = 0f;

    private Quaternion targetRotation;

    void Update()
    {
        // Movimiento hacia adelante/atr�s
        transform.Translate(Vector3.forward * forwardInput * moveSpeed * Time.deltaTime);

        // Movimiento hacia arriba/abajo
        transform.Translate(Vector3.up * verticalInput * verticalSpeed * Time.deltaTime);

        // Inclinaci�n lateral con aceler�metro
        Vector3 tilt = Input.acceleration;
        float tiltX = Mathf.Clamp(tilt.x, -1f, 1f);  // lateral izquierda/derecha

        // ROTACI�N SUAVE
        float yaw = tiltX * turnSpeed * Time.deltaTime; // giro leve
        transform.Rotate(0f, yaw, 0f); // rotamos sobre eje Y

        // C�lculo de inclinaci�n natural
        float targetRoll = -tiltX * maxTiltAngle;           // girar visualmente a los lados
        float targetPitch = -verticalInput * maxPitchAngle; // inclinar nariz al subir/bajar

        // Armamos la rotaci�n deseada
        targetRotation = Quaternion.Euler(targetPitch, transform.eulerAngles.y, targetRoll);

        // Interpolamos hacia esa rotaci�n suavemente
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness);
    }

    public void SetForward(float value)
    {
        forwardInput = value;
    }

    public void SetVertical(float value)
    {
        verticalInput = value;
    }
}
