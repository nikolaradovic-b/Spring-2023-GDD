using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected Transform firingOrigin = null;
    [SerializeField] protected float fireInterval = 2.0f;
    [SerializeField] protected float seePlayerRange = 7f;
    [SerializeField] protected float attackRange = 5.0f;
    [SerializeField] protected int healDelay = 2;
    [SerializeField] protected int healRate = 1;

    protected Rigidbody2D rb;
    protected PlayerMovement player;
    protected EnemyMovement enemyMovement;

    protected bool seePlayer = false;
    protected float fireTimer;
    protected bool attacking = false;
    protected float healStart;

    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = transform.parent.GetComponent<EnemyMovement>();
        fireTimer = fireInterval;
        healStart = Time.time;
    }

    protected virtual void Update()
    {
        if (!attacking)
        {
            if (Time.time > healStart && gameObject.tag != "Boss")
            {
                GetComponent<Health>().Heal(healRate);
                healStart = Time.time + healDelay;
            }
        }
    }

    public void SetAttacking(bool attacking)
    {
        this.attacking = attacking;
    }

    public virtual void ExecuteFireState()
    {
        PreFire();
    }

    protected virtual void PreFire()
    {
        if (player != null)
        {
            enemyMovement.AdjustFaceDirectionIfMelee(player.transform.position);
            enemyMovement.StopMovement();
        }
    }

    public virtual void ExecuteChaseState()
    {
        enemyMovement.AdjustFaceDirectionIfMelee(player.transform.position);
        enemyMovement.MoveTo(player.transform, true);
    }

    public float GetSeePlayerRange()
    {
        return seePlayerRange;
    }

    public float GetAttackRange()
    {
        return attackRange;
    }

    public virtual string toString()
    {
        return "Enemy";
    }

}
