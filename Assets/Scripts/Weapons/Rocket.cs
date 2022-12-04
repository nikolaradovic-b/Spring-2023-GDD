using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Gun
{
    public Transform firingOrigin = null;
    public GameObject bulletPrefab = null;
    public GameObject rocketPrefab = null;
    public float bulletForce = 10f;
    public new int ammo = 5;
    public new int maxAmmo = 5;
    private float fireDelay = 1.0f;
    public new int reloadAmount = 5;

    private float fireDelayStart = 0f;

    public Rocket(Transform fo, GameObject bp, GameObject rp, int startammo) {
        firingOrigin = fo;
        bulletPrefab = bp;
        rocketPrefab = rp;
        ammo = startammo;
    }

    // Update is called once per frame
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
    }

    public void Shoot()
    {
        if (Time.time < fireDelayStart) {return;}
        GameObject bulletInstance = Instantiate(bulletPrefab, firingOrigin.position, firingOrigin.rotation * Quaternion.AngleAxis(90, Vector3.forward));
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        rb.AddForce(-1 * firingOrigin.up * bulletForce, ForceMode2D.Impulse);
        ammo -= 1;
        fireDelayStart = Time.time + fireDelay;
    }

    public override void Reload() {
        ammo = Mathf.Min(ammo + reloadAmount, maxAmmo);
    }

    public override int ammoCount() {
        return ammo;
    }

    public override int maxAmmoCount() {
        return maxAmmo;
    }

    public override void drop() {
        Debug.Log("in rocket drop");
        Instantiate(rocketPrefab, firingOrigin.position, firingOrigin.rotation);
        FindObjectOfType<GameManager>().rocketAmmo = ammo;
    }
}