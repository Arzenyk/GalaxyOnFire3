using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 100;

    void Update()
    {
        // Mueve la nave hacia adelante multiplicando su valor z por la velocidad.

        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }
}
