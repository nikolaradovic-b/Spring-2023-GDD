using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;

    private int currentHealth;
    private bool immuneState = false;
    private float immuneTimer;
    private float immuneDuration = 2.0f;

    public static Action<GameObject> onTakeDamage;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update(){
        if (immuneTimer > Mathf.Epsilon){
            immuneTimer = Mathf.Max(0f, immuneTimer - Time.deltaTime);
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if(immuneState && immuneTimer > Mathf.Epsilon){
            amount = 0;
        } else {
            immuneState = false;
            immuneTimer = 0f;
        }

        currentHealth = Mathf.Max(0, currentHealth - amount);
        onTakeDamage?.Invoke(gameObject);
        if (currentHealth == 0)
        {
            if (GetComponent<PlayerMovement>())
            {
                // Player has died!
                Debug.Log("Lost!");
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
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }

    public void setImmuneState()
    {
        immuneState = true;
        immuneTimer = immuneDuration;
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
