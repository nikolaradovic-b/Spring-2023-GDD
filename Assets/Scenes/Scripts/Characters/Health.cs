using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BulletType
{
    Bullet,
    Spikeball,
    Rocket
}

public class Health : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 10;
    [SerializeField] protected float immuneDuration = 2f;
    [Range(0f, 1f)][SerializeField] protected float immuneAlpha = 0.3f;
    [SerializeField] protected int coinRewardOnDie = 10;

    protected int currentHealth;
    protected float immuneTimer;
    protected SpriteRenderer rend;

    public static Action<GameObject> onTakeDamage;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        rend = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (immuneTimer > 0f)
        {
            immuneTimer = Mathf.Max(0f, immuneTimer - Time.deltaTime);
        }
        else
        {
            var color = rend.color;
            color.a = 1f;
            rend.color = color;
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public void SetImmune()
    {
        immuneTimer = immuneDuration;
        var color = rend.color;
        color.a = immuneAlpha;
        rend.color = color;
    }

    public virtual void TakeDamage(int amount, BulletType type = BulletType.Bullet)
    {
        if (immuneTimer > 0f)
        {
            return;
        } 

        currentHealth = Mathf.Max(0, currentHealth - amount);
        onTakeDamage?.Invoke(gameObject);

        if (currentHealth == 0)
        {
            if (GetComponent<PlayerMovement>())
            {
                // Player has died!
                Destroy(gameObject);
                FindObjectOfType<GameManager>().RestartLevel();
            }
            else if (GetComponent<KeyCrate>())
            {
                GetComponent<KeyCrate>().DeathSequence();
                Destroy(gameObject);
            }
            else
            {
                // Enemy has died
                FindObjectOfType<GameManager>().DestroyEnemy();
                FindObjectOfType<Inventory>().GainCoins(coinRewardOnDie);
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
