using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(0, currentHealth - amount);
        if (currentHealth == 0)
        {
            if (GetComponent<PlayerMovement>())
            {
                // Player has died!
                Debug.Log("Lost!");
                Destroy(gameObject);
            }
            else
            {
                // Enemy has died
                FindObjectOfType<GameManager>().DestroyEnemy();
                Destroy(gameObject);
            }
        }
    }
}
