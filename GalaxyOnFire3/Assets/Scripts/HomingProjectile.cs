using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public float homingRadius = 10f;      // Distance to start homing
    public float homingStrength = 5f;     // How strongly it turns toward the enemy
    public float speed = 20f;             // Projectile speed

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Find the closest enemy within the homing radius
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = homingRadius;

        foreach (var enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            // Calculate direction to the enemy
            Vector3 direction = (closestEnemy.transform.position - transform.position).normalized;
            // Adjust velocity to home in on the enemy
            Vector3 newVelocity = Vector3.Lerp(rb.velocity.normalized, direction, Time.fixedDeltaTime * homingStrength) * speed;
            rb.velocity = newVelocity;
        }
        else
        {
            // Keep moving forward at constant speed
            rb.velocity = rb.velocity.normalized * speed;
        }
    }
}
