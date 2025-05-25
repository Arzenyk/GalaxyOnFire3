using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    public GameObject Escudo;
    private bool controlsLocked = false;
    public PlayerMovement playerMovement;
    public bool escudoActive;

    // Este método se llama desde el botón de Escudo
    public void Shield()
    {
        if (controlsLocked)
            return;

        Invoke(nameof(DeactivateObject), 3f);
        Escudo.SetActive(true);
        escudoActive = true;
    }
    public void DeactivateObject()
    {
        Escudo.SetActive(false); // Deactivate
        escudoActive = false;
    }

    public void LockControls()
    {
        controlsLocked = true;
    }
}
