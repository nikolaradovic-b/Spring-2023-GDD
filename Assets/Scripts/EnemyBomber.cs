using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : EnemyShooterBase
{
    [SerializeField] private float chaseSpeedMultiplier = 2f;
    [SerializeField] private int explosionDamage = 5;
    [SerializeField] private GameObject explosionVFX = null;
    [SerializeField] private EnemyMovement enemyMovement = null;

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
        if (seePlayer)
        {
            enemyMovement.MoveTo(player.transform.position, chaseSpeedMultiplier);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.tag == "Player")
        {
            GameObject vfx = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            vfx.layer = gameObject.layer;
            vfx.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;
            Destroy(vfx, 2f);

            collision.gameObject.GetComponent<Health>().TakeDamage(explosionDamage);
            Destroy(transform.parent.gameObject);
        }
    }
}
