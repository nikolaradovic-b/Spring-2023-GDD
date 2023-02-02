using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public abstract class EnemyShooterBase : EnemyBase
{
    [SerializeField] protected GameObject bulletPrefab = null;
    [SerializeField] protected float bulletForce = 20f;
    [SerializeField] protected GameObject strafeWaypoint;
    [SerializeField] protected float dodgeCooldown = 1f; //cool down for strafing

    protected bool dodgeLeft = true;
    protected bool isHorizontalDodge = true;
    protected Vector3 offset; 
    protected float timeUntilNextDodge;
    private bool timerRunning = false;
    private static Random rnd = new Random();
    protected bool toStrafe = false;

    protected override void Start()
    {
        base.Start();
        timeUntilNextDodge = dodgeCooldown;
    }

    protected override void Update()
    {
        base.Update();
        if (toStrafe && strafeWaypoint != null)
        {
            //Strafe();
        }
    }

    public override void ExecuteFireState()
    {
        base.ExecuteFireState();
        if (fireTimer <= Mathf.Epsilon)
        {
            var fireDir = (player.transform.position - transform.position).normalized;
            GameObject bulletInstance = Instantiate(bulletPrefab, firingOrigin.position, firingOrigin.rotation);
            float angle = Mathf.Atan2(fireDir.y, fireDir.x) * Mathf.Rad2Deg - 90f;
            Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
            rb.AddForce(fireDir * bulletForce, ForceMode2D.Impulse);
            rb.rotation = angle;
            fireTimer = fireInterval;
        }
        else if (fireTimer > Mathf.Epsilon)
        {
            fireTimer = Mathf.Max(0f, fireTimer - Time.deltaTime);
        }
    }

    private void Strafe()
    {
        timeUntilNextDodge -= Time.deltaTime;
        if (timeUntilNextDodge <= 0f)
        {
            Dodge();
        }
    }

    protected void Dodge()
    {
        if (Vector2.Distance(transform.position, strafeWaypoint.transform.position) <= 0.5f)
        {
            timeUntilNextDodge = dodgeCooldown;
            float offsetAmount = (float)rnd.NextDouble();
            if (dodgeLeft == true)
            {
                offsetAmount = -offsetAmount;
                dodgeLeft = false;
            }
            else
            {
                dodgeLeft = true;
            }
            offset = orientation(offsetAmount, rnd.Next(1));
            Vector3 newPos = transform.position + offset;
            strafeWaypoint.transform.position = newPos;
        }
        //transform.parent.GetComponent<EnemyMovement>().MoveTo(strafeWaypoint.transform, false, true);
    }

    /* Choosing to strafe on x or y axis */
    private Vector3 orientation(float offset, int isHorizontal)
    {
        if (isHorizontal == 0)
        {
            return new Vector3(offset, 0, 0);
        }
        else
        {
            return new Vector3(0, offset, 0);
        }
    }

    public void SetStrafe(bool strafe)
    {
        //this.toStrafe = strafe;
    }

    public override string toString(){
        return "EnemyShooterBase";
    }
}   

