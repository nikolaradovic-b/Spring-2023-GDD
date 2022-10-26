using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeEnemyShooter : EnemyShooterBase
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FirePlayerIfSeen()
    {
        if (seePlayer && fireTimer <= Mathf.Epsilon)
        {
            Debug.Log("Fire!");
            GameObject bulletInstance = Instantiate(bulletPrefab, firingOrigin.position, firingOrigin.rotation);
            Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
            rb.AddForce(-1 * firingOrigin.up * bulletForce, ForceMode2D.Impulse);
            fireTimer = fireInterval;
        }
        else if (seePlayer && fireTimer > Mathf.Epsilon)
        {
            fireTimer = Mathf.Max(0f, fireTimer - Time.deltaTime);
        }
    }
}