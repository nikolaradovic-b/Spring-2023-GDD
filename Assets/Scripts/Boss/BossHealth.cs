using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossHealth : Health
{
    [SerializeField] private float vulnerableHealthThreshold = 60f;
    [SerializeField] private float spawnMinionThreshold = 40f;
    [SerializeField] private float protectionThreshold = 10f;

    public static Action onVulnerable;
    public static Action onSpawnMinion;
    public static Action onProtect;

    private Boss boss;

    protected override void Start()
    {
        base.Start();
        boss = GetComponent<Boss>();
    }

    public override void TakeDamage(int amount, BulletType type = BulletType.Bullet)
    {
        switch (type)
        {
            case BulletType.Bullet:
                TakeBulletDamage(amount);
                break;
            case BulletType.Spikeball:
                TakeSpikeBallDamage(amount);
                break;
            case BulletType.Rocket:
                TakeRocketDamage(amount);
                break;
            default:
                break;
        }
        float healthPercent = currentHealth * 100 / maxHealth;
        if (healthPercent <= protectionThreshold)
        {
            // If health is less than 10%, go into protection state
            onProtect?.Invoke();
        }
        else if (healthPercent <= spawnMinionThreshold)
        {
            // If health is less than 40%, go into spawn minion state
            onSpawnMinion?.Invoke();
        }
        else if (healthPercent <= vulnerableHealthThreshold)
        {
            // If health is less than 60%, go into vulnerable state
            onVulnerable?.Invoke();
        }
    }

    private void TakeRocketDamage(int amount)
    {
        currentHealth = Mathf.Max(0, currentHealth - amount);
        onTakeDamage?.Invoke(gameObject);

        if (currentHealth == 0)
        {
            // Boss has died
            FindObjectOfType<GameManager>().DefeatBoss();
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    private void TakeBulletDamage(int amount)
    {
        if (boss.GetPhase() != BossPhase.Vulnerable)
        {
            return;
        }

        currentHealth = Mathf.Max(0, currentHealth - amount);
        onTakeDamage?.Invoke(gameObject);

        if (currentHealth == 0)
        {
            // Boss has died
            FindObjectOfType<GameManager>().DefeatBoss();
            Destroy(gameObject.transform.parent.gameObject);
        }
    }


    private void TakeSpikeBallDamage(int amount)
    {
        if (boss.GetPhase() == BossPhase.Protection)
        {
            return;
        }

        currentHealth = Mathf.Max(0, currentHealth - amount);
        onTakeDamage?.Invoke(gameObject);

        if (currentHealth == 0)
        {
            // Boss has died
            FindObjectOfType<GameManager>().DefeatBoss();
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
