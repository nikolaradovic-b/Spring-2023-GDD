using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : EnemyBase
{
    [SerializeField] private int attackDamage = 3;

    protected Animator m_animator;

    protected override void Start()
    {
        m_animator = GetComponent<Animator>();
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void ExecuteChaseState()
    {
        base.ExecuteChaseState();
        m_animator.SetInteger("AnimState", 2);
    }

    public override void ExecuteFireState()
    {
        base.ExecuteFireState();

        if (fireTimer <= Mathf.Epsilon)
        {
            m_animator.SetTrigger("Attack");
            fireTimer = fireInterval;
        }
        else if (fireTimer > Mathf.Epsilon)
        {
            fireTimer = Mathf.Max(0f, fireTimer - Time.deltaTime);
            m_animator.SetInteger("AnimState", 1);
        }
    }

    // Called by animation events
    public void ApplyMeleeDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(firingOrigin.position, attackRange);
        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponent<PlayerMovement>())
            {
                //Debug.Log("Melee Hit!");
                player.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }
    }

    public override string toString()
    {
        return "EnemyMeleeBase";
    }
}
