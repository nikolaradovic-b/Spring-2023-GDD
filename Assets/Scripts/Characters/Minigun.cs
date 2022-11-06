using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : MonoBehaviour
{
    [SerializeField] private Transform firingOrigin = null;
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private float ammo = 100f;
    [SerializeField] private float maxAmmo = 100f;
    [SerializeField] private float fireDelay = 0.5f;

    private float fireDelayStart = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
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

    private void Shoot()
    {
        if (Time.time < fireDelayStart) {return;}
        GameObject bulletInstance = Instantiate(bulletPrefab, firingOrigin.position, firingOrigin.rotation);
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        rb.AddForce(-1 * firingOrigin.up * bulletForce, ForceMode2D.Impulse);
        ammo -= 1;
        fireDelayStart = Time.time + fireDelay;
    }
    
    public void Reload(int amount)
    {
        ammo = Mathf.Min(ammo + amount, maxAmmo);
    }
}
