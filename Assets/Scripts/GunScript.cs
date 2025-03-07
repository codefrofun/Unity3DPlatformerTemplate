using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint; // Empty GameObject at the muzzle
    public float fireRate = 0.3f; // Time between shots

    private float nextFireTime = 0f; 

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

        if (rb != null)
        {
            rb.velocity = bullet.transform.forward * fireRate; // Moves in the correct direction
            Debug.Log("Bullet Velocity: " + rb.velocity);
        }
    }
}