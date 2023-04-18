using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : Bullet
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == tagToAvoid || collision.gameObject.tag == tagToAvoid2) { return; }
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.layer = gameObject.layer;
        vfx.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;
        if (collision.gameObject.GetComponent<Health>())
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damageDealt, BulletType.Rocket);
        }

        Destroy(vfx, 1f);
        Destroy(gameObject);
    }
}
