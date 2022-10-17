using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : EnemyBase
{
    [SerializeField] private GameObject hitVFX = null;

    protected override void Start()
    {
        base.Start();
        attackRange = 0.0f;
        speedMultiplier = 2.0f;
        damage = 5;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
            vfx.layer = gameObject.layer;
            vfx.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;
            Destroy(gameObject);
        }
    }

    public override string ToString()
    {
        return "EnemyBomber";
    }
}