using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : EnemyBase
{
    [SerializeField] private int attackDamage = 3;

    protected Animator m_animator;
    /*private float attackRadius;
    private Vector3 prevPosition;*/


    protected override void Start()
    {
        m_animator = GetComponent<Animator>();
        base.Start();
        //prevPosition = transform.position;
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
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (attacking && fireTimer <= Mathf.Epsilon && distance <= attackRange)
        {
            m_animator.SetTrigger("Attack");
            fireTimer = fireInterval;
        }
        else if (attacking && fireTimer > Mathf.Epsilon && distance <= attackRange)
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
                Debug.Log("Melee Hit!");
                player.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }
    }

    public override string toString()
    {
        return "EnemyMeleeBase";
    }
}
