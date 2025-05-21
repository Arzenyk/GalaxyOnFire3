using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 3f;
    public GameObject explosionPrefab;
    public MeshRenderer MeshRenderer;
    public Text perdiste;
    public Button reintentar;

    void OnTriggerEnter(Collider other)
    {
        // Instanciamos la explosión en la posición actual
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        if (other.CompareTag("EnemyProjectile"))
        {
            Destroy(other.gameObject);
            MeshRenderer.enabled = false;
            //Time.timeScale = 0;
            perdiste.text = "Perdiste";
            reintentar.enabled = true;
        }

        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            MeshRenderer.enabled = false;
            //Time.timeScale = 0;
            perdiste.text = "Perdiste";
            reintentar.enabled = true;
        }
    }

    void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Reintentar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
