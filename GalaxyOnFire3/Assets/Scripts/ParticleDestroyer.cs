using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    public class AutoDestroy : MonoBehaviour
    {
        void Start()
        {
            Destroy(gameObject, 2f); // Se destruye después de 2 segundos
        }
    }
}
