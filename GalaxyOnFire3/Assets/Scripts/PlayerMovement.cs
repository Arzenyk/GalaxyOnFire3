using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    // Velocidad de movimiento hacia adelante/atr�s
    public float moveSpeed = 10f;

    // Velocidad de movimiento vertical (subir/bajar)
    public float verticalSpeed = 5f;

    // Velocidad de giro lateral usando el aceler�metro
    public float turnSpeed = 50f;

    // Variables internas que se activan al presionar los botones UI
    private float forwardInput = 0f;
    private float verticalInput = 0f;

    void Update()
    {
        // Mover la nave hacia adelante o atr�s seg�n el input
        // Vector3.forward es el eje Z local de la nave
        transform.Translate(Vector3.forward * forwardInput * moveSpeed * Time.deltaTime);

        // Mover la nave hacia arriba o abajo seg�n el input
        // Vector3.up es el eje Y local
        transform.Translate(Vector3.up * verticalInput * verticalSpeed * Time.deltaTime);

        // Detectar la inclinaci�n del celular (aceler�metro)
        Vector3 tilt = Input.acceleration;

        // Usamos tilt.x para rotar la nave en el eje Y (giro lateral)
        float yaw = tilt.x * turnSpeed * Time.deltaTime;

        // Aplicamos la rotaci�n en el eje Y (yaw)
        transform.Rotate(0f, yaw, 0f);
    }

    // Este m�todo lo llama el bot�n de avanzar o retroceder
    // El par�metro "value" puede ser:
    // 1 para avanzar, -1 para retroceder, 0 para detenerse
    public void SetForward(float value)
    {
        forwardInput = value;
    }

    // Este m�todo lo llama el bot�n de subir o bajar
    // El par�metro "value" puede ser:
    // 1 para subir, -1 para bajar, 0 para detenerse
    public void SetVertical(float value)
    {
        verticalInput = value;
    }
}
