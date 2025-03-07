using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 65f; // How fast the bullet will shoot
    public int damage = 2; // Bullet damage, enemies will need multiple hits

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // Prevents passing through objects
        rb.velocity = transform.forward * speed; // Moves the bullet forward
        Destroy(gameObject, 12f);// Destroy after 10 sec

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HealthController enemyHealth = other.GetComponent<HealthController>();
            if (enemyHealth != null)
            {
                enemyHealth.ApplyDamage(damage);
            }
        }

        // Destroy on any impact (except Player)
        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}