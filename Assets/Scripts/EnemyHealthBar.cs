using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Health health = null;

    private Image healthBar;

    private void Start()
    {
        healthBar = GetComponent<Image>();
    }

    private void Update()
    {
        var curScale = healthBar.transform.localScale;
        curScale.x = health.GetCurrentHealth() / health.GetMaxHealth();
        healthBar.transform.localScale = curScale;
    }
}
