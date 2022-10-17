using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeBase : EnemyBase
{
    protected Animator m_animator;

    private LayerMask playerLayer;
    private Vector3 prevPosition;


    protected override void Start()
    {
        m_animator = GetComponent<Animator>();
        base.Start();
        fireInterval = 2.0f;
        attackRange = 0.7f;
        damage = 2;
        prevPosition = transform.position;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FollowPLayerIfSeen()
    {
        base.FollowPLayerIfSeen();

        float distance = Vector3.Distance(rb.transform.position, prevPosition);
        if (Mathf.Abs(distance) > Mathf.Abs(firingOrigin.position.x))
        {
            m_animator.SetInteger("AnimState", 2);
        } else {
            m_animator.SetInteger("AnimState", 1);
        }
    }

    protected override void FirePlayerIfSeen()
    {
        if (attacking && fireTimer <= Mathf.Epsilon)
        {
            Debug.Log("Melee!");
            m_animator.SetTrigger("Attack");
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(firingOrigin.position, attackRange, playerLayer);
            foreach (Collider2D player in hitPlayers)
            {
                player.GetComponent<Health>().TakeDamage(damage);
            }
            fireTimer = fireInterval;
        }
        else if (attacking && fireTimer > Mathf.Epsilon)
        {
            fireTimer = Mathf.Max(0f, fireTimer - Time.deltaTime);
        }
    }

    protected override void FacePlayerIfSeen()
    {
        if (seePlayer)
        {
            Debug.Log("See!");
            Vector2 playerPos = player.gameObject.transform.position;
            Vector2 lookDirection = playerPos - rb.position;
            transform.localScale = Mathf.Atan2(lookDirection.y, lookDirection.x) > 0 ? new Vector3(1.0f, 1.0f, 1.0f) : new Vector3(-1.0f, 1.0f, 1.0f);
        }
    }

    void OnDrawGizmosSelected(){
        if(firingOrigin == null) return;
        Gizmos.DrawWireSphere(firingOrigin.position, attackRange);
    }

    public override string toString()
    {
        return "EnemyMeleeBase";
    }
}
