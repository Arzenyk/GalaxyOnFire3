using Unity.VisualScripting;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    public GameObject Escudo;
    private bool controlsLocked = false;
    public PlayerMovement playerMovement;

    // Este m�todo se llama desde el bot�n de Escudo
    public void Shield()
    {
        if (controlsLocked)
            return;

        Invoke("DeactivateObject", 3f);
        Escudo.gameObject.SetActive(true);
    }
    void DeactivateObject()
    {
        Escudo.gameObject.SetActive(false); // Deactivate after 3 seconds
    }

    public void LockControls()
    {
        controlsLocked = true;
    }
}
