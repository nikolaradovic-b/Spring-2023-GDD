using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform firingOrigin = null;
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private float bulletForce = 20f;

    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private int ammo = 10;
    [SerializeField] private int rechargeRate = 1;
    [SerializeField] private int rechargeDelay = 5;
    [SerializeField] private int chargeDelay = 1;

    private float rechargeStart = 0f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (ammo > 0) {
                Shoot();
                Debug.Log(ammo);
            } else {
                Debug.Log("Out of Ammo");
            }
        }

        Recharge();
    }

    private void Shoot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, firingOrigin.position, firingOrigin.rotation);
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        rb.AddForce(-1 * firingOrigin.up * bulletForce, ForceMode2D.Impulse);
        ammo = ammo - 1;
        rechargeStart = Time.time + rechargeDelay;
    }

    private void Recharge()
    {
        if (Time.time > rechargeStart && ammo < maxAmmo)
        {
            ammo = Math.Min(maxAmmo, ammo + rechargeRate);
            rechargeStart = rechargeStart + chargeDelay;
            Debug.Log("+1 Ammo");
        }
    }
}
