using Unity.VisualScripting;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    public GameObject Escudo;

    // Este método se llama desde el botón de Escudo
    public void Shield()
    {
        Invoke("DeactivateObject", 3f);
        Escudo.gameObject.SetActive(true);
    }
    void DeactivateObject()
    {
        Escudo.gameObject.SetActive(false); // Deactivate after 3 seconds
    }
}
