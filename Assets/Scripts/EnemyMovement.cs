using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float patrolSpeed = 1.5f;
    [SerializeField] private float arrivalOffsetAllowed = 0.25f;

    private GameObject nextWaypoint;
    private Rigidbody2D rb;
    private EnemyBase enemy;

    private int currentWaypointIndex;
    private bool startingMoving = true;
    private Vector3 faceDir;

    private void Start()
    {
        FindObjectOfType<GameManager>().RegisterEnemy();

        // Initialize enemy location and variables
        int randomIndex = Random.Range(0, waypoints.Length);
        currentWaypointIndex = randomIndex;
        GameObject initialWaypoint = waypoints[randomIndex];
        transform.position = initialWaypoint.transform.position;
        nextWaypoint = initialWaypoint;
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<EnemyBase>();
    }

    private void Update()
    {
        if (enemy.GetIsAttacking()) { return; }
        float distToNextWaypoint =
            Vector2.Distance(transform.position, nextWaypoint.transform.position);
        if (distToNextWaypoint <= arrivalOffsetAllowed)
        {
            startingMoving = false;
            // Arrive at nextWaypoint, pick a new waypoint and start going there
            int newIndex = Random.Range(0, waypoints.Length);
            while (newIndex == currentWaypointIndex)
            {
                newIndex = Random.Range(0, waypoints.Length);
            }
            currentWaypointIndex = newIndex;
            nextWaypoint = waypoints[currentWaypointIndex];
            StartCoroutine(PauseThenStart());
        }
        else if (startingMoving)
        {
            // move to next waypoint
            MoveTo(nextWaypoint.transform.position, 1f);
        }
        faceDir = (nextWaypoint.transform.position - transform.position).normalized;
        if (enemy.toString() == "EnemyMeleeBase")
        {
            enemy.transform.localScale = Mathf.Atan2(faceDir.y, faceDir.x) > 0 ? new Vector3(1.0f, 1.0f, 1.0f) : new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else
        {
            float angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
    }

    public void MoveTo(Vector2 position, float multiplier)
    {
        transform.position = Vector2.MoveTowards(
                transform.position,
                position,
                Time.deltaTime * patrolSpeed * multiplier);
    }

    private IEnumerator PauseThenStart()
    {
        yield return null;
        startingMoving = true;
    }
}
