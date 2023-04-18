using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossPhase
{
    Immune,
    Vulnerable,
    MinionSpawn,
    Protection
}

public class Boss : EnemyBase
{
    [SerializeField] private GameObject scatterProjectile = null;
    [Tooltip("Smaller=Faster")]
    [SerializeField] private float fireRateIncrease = 0.6f;
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private GameObject shield = null;
    [SerializeField] private Color protectionColor;

    private BossPhase phase;
    private bool canFire = false;

    protected override void Start()
    {
        base.Start();
        phase = BossPhase.Immune;
    }

    private void OnEnable()
    {
        BossHealth.onVulnerable += SwitchToVulnerable;
        BossHealth.onSpawnMinion += SwitchToSpawnMinion;
        BossHealth.onProtect += SwitchToProtect;
    }

    private void OnDisable()
    {
        BossHealth.onVulnerable -= SwitchToVulnerable;
        BossHealth.onSpawnMinion -= SwitchToSpawnMinion;
        BossHealth.onProtect += SwitchToProtect;
    }

    private void SwitchToProtect()
    {
        if (phase != BossPhase.Protection)
        {
            phase = BossPhase.Protection;
            shield.GetComponent<SpriteRenderer>().color = protectionColor;
            canFire = false;
        }
    }

    private void SwitchToSpawnMinion()
    {
        if (phase != BossPhase.MinionSpawn)
        {
            phase = BossPhase.MinionSpawn;
            shield.SetActive(true);
            fireInterval = fireInterval / fireRateIncrease;
        }
    }

    private void SwitchToVulnerable()
    {
        if (phase != BossPhase.Vulnerable)
        {
            phase = BossPhase.Vulnerable;
            fireInterval = fireRateIncrease * fireInterval;
            shield.SetActive(false);
        }
    }

    protected override void Update()
    {
        if (phase == BossPhase.Protection)
        {
            return;
        }

        base.Update();
        if (fireTimer <= Mathf.Epsilon)
        {
            canFire = true;
            fireTimer = fireInterval;
        }
        else if (fireTimer > Mathf.Epsilon)
        {
            fireTimer = Mathf.Max(0f, fireTimer - Time.deltaTime);
        }
    }

    public BossPhase GetPhase()
    {
        return phase;
    }

    public bool GetCanFire()
    {
        return canFire;
    }

    public override void ExecuteFireState()
    {
        if (phase == BossPhase.Protection)
        {
            return;
        }

        base.ExecuteFireState();
        GameObject bulletInstance = Instantiate(scatterProjectile, firingOrigin.position, firingOrigin.rotation);
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.down * bulletForce, ForceMode2D.Impulse);
        canFire = false;
    }
}
