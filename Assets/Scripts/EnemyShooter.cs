using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private Transform firingOrigin = null;
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private float fireInterval = 2f;
    [SerializeField] private float playerProximityLimit = 10f;

    private Rigidbody2D rb;
    private PlayerMovement player;

    private bool seePlayer = false;
    private float fireTimer;
    private bool shooting = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        fireTimer = fireInterval;
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        CheckPlayerProximity();
        FacePlayerIfSeen();
        FirePlayerIfSeen();
    }

    public bool GetIsShooting()
    {
        return shooting;
    }

    private void FacePlayerIfSeen()
    {
        if (seePlayer)
        {
            Debug.Log("See!");
            Vector2 playerPos = player.gameObject.transform.position;
            Vector2 lookDirection = playerPos - rb.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
    }

    private void FirePlayerIfSeen()
    {
        if (seePlayer && fireTimer <= Mathf.Epsilon)
        {
            Debug.Log("Fire!");
            GameObject bulletInstance = Instantiate(bulletPrefab, firingOrigin.position, firingOrigin.rotation);
            Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
            rb.AddForce(-1 * firingOrigin.up * bulletForce, ForceMode2D.Impulse);
            fireTimer = fireInterval;
        }
        else if (seePlayer && fireTimer > Mathf.Epsilon)
        {
            fireTimer = Mathf.Max(0f, fireTimer - Time.deltaTime);
        }
    }

    private void CheckPlayerProximity()
    {
        if (player.gameObject.layer == gameObject.layer)
        {
            // same layer, then check distance
            if (Vector2.Distance(player.gameObject.transform.position, transform.position) < 
                playerProximityLimit)
            {
                // player in range
                seePlayer = true;
                shooting = true;
                return;
            }
        }
        seePlayer = false;
        shooting = false;
    }
}
