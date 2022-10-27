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

    protected override void FollowPLayerIfSeen()
    {

    }

    protected override void FirePlayerIfSeen()
    {
        if (seePlayer)
        {
            var player = FindObjectOfType<PlayerMovement>();
            //transform.parent.GetComponent<EnemyMovement>().MoveTo(player.transform.position, speedMultiplier);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
            vfx.layer = gameObject.layer;
            vfx.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;
            Destroy(vfx, 5f);
            Destroy(transform.parent.gameObject);
        }
    }

    public override string ToString()
    {
        return "EnemyBomber";
    }
}