using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public Transform firingOrigin = null;
    public GameObject bulletPrefab = null;
    public float bulletForce = 20f;
    public new int ammo = 20;
    public new int maxAmmo = 20;
    private int rechargeRate = 1;
    private int rechargeDelay = 5;
    private int chargeDelay = 1;
    public new int reloadAmount = 20;

    private float rechargeStart = 0f;

    public Pistol(Transform fo, GameObject bp) {
        firingOrigin = fo;
        bulletPrefab = bp;
    }

    public override void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (ammo > 0)
            {
                Shoot();
            }
            else
            {
                Debug.Log("Out of Ammo");
            }
        }
        Recharge();
    }

    public void Shoot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, firingOrigin.position, firingOrigin.rotation);
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        rb.AddForce(-1 * firingOrigin.up * bulletForce, ForceMode2D.Impulse);
        ammo -= 1;
        rechargeStart = Time.time + rechargeDelay;

    }

    private void Recharge()
    {
        if (Time.time > rechargeStart && ammo < maxAmmo)
        {
            ammo = Mathf.Min(maxAmmo, ammo + rechargeRate);
            rechargeStart = Time.time + chargeDelay;
            Debug.Log("+1 Ammo");
        }
    }
}
