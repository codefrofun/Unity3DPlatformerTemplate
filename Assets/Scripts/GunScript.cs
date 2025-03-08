using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint; // Empty GameObject at the muzzle
    public float fireRate = 0.3f; // Time between shots
    public AudioClip shootSound;

    private AudioSource audioSource;
    private float nextFireTime = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the Audio Source
    }

    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame && Time.time >= nextFireTime) // Left button on the mouse
        {
            Shoot(); // Bullet shoot method
            nextFireTime = Time.time + fireRate;  // Cooldown between shots
        }
    }


    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        float bulletSpeed = 50f;

        if (rb != null)
        {
            // Get the mouse position in the world
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            Vector3 targetPoint;

            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;

            }
            else
            {
                targetPoint = ray.GetPoint(100f); // If nothing hit, shoot bullet far into distance
            }

            // Calculate direction from gun to target
            Vector3 shootDirection = (targetPoint - firePoint.position).normalized;

            // Apply velocity in that direction
            rb.velocity = shootDirection * bulletSpeed;

            Debug.Log("Bullet Direction: " + shootDirection);
        }

            if (shootSound != null && audioSource != null)
            {
            audioSource.PlayOneShot(shootSound);
            }
    }
}