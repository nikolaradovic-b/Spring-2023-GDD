using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikeball : MonoBehaviour
{
    [SerializeField] private float speedToDealDamage = 5f;
    [SerializeField] private int damageDealt = 5;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Can only hurt boss and must be above a certain speed
        if (collision.gameObject.tag == "Boss" && 
            rb.velocity.magnitude > speedToDealDamage)
        {
            collision.gameObject.GetComponent<BossHealth>().TakeDamage(damageDealt, BulletType.Spikeball);
        }
    }
}
