using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float patrolSpeed = 5f;
    [SerializeField] private float arrivalOffsetAllowed = 2f;

    private GameObject nextWaypoint;
    private Rigidbody2D rb;
    private EnemyShooterBase shooter;

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
        rb = GetComponentInChildren<Rigidbody2D>();
        shooter = GetComponentInChildren<EnemyShooterBase>();
    }

    private void Update()
    {
        if (shooter.GetIsShooting()) { return; }
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
        float angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
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