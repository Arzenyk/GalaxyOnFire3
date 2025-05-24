using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public DynamicCamera cameraScript;

    private bool controlsLocked = false;

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

    public Slider forwardSlider; // Asignar en el Inspector
    public Slider verticalSlider; // Asignar en el Inspector
    public Button escudo; // Asignar en el Inspector
    public Button disparo; // Asignar en el Inspector
    public ParticleSystem particulasVelocidad;

    void Start()
    {
        if (cameraScript == null)
        {
            // Intenta encontrar autom�ticamente el script en la c�mara principal
            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                cameraScript = mainCam.GetComponent<DynamicCamera>();
                if (cameraScript == null)
                {
                    Debug.LogError("La Main Camera no tiene el script DynamicCamera.");
                }
            }
            else
            {
                Debug.LogError("No se encontr� ninguna Main Camera en la escena.");
            }
        }
    }


    void Update()
    {
        if (controlsLocked)
            return;

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

    public void LockControls()
    {
        controlsLocked = true;
        verticalSlider.interactable = false;
        forwardSlider.interactable = false;
        escudo.interactable = false;
        disparo.interactable = false;
        particulasVelocidad.gameObject.SetActive(false);
        forwardInput = 0f;
        verticalInput = 0f;
    }

    public void SetForward(float value)
    {
        forwardInput = value;

        if (cameraScript != null)
        {
            cameraScript.SetBoost(value > 0.1f);
            forwardInput = value * 10;
        }
        else
        {
            Debug.LogWarning("cameraScript no est� asignado en PlayerMovement.");
        }
    }


    public void SetVertical(float value)
    {
        verticalInput = value;
    }
}
