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
    public PlayerMovement playerMovement;
    public PlayerShield playerShield;
    public PlayerShooting playerShooting;
    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            Destroy(other.gameObject);
            TakeDamage(1);
        }

        if (other.CompareTag("Enemy"))
        {
            //Destroy(other.gameObject);
            TakeDamage(1);
        }
    }

    void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            if (playerMovement != null)
            {
                explosionPrefab.SetActive(true);
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                playerMovement.LockControls();
                playerShooting.LockControls();
                playerShield.LockControls();
                //player.tag = "Untagged";
            }
            MeshRenderer.enabled = false;
            perdiste.text = "Perdiste";
            reintentar.gameObject.SetActive(true);

        }
    }

    public void Reintentar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
