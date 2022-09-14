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
    private EnemyShooter shooter;

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
        shooter = GetComponent<EnemyShooter>();
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
            transform.position = Vector2.MoveTowards(
                transform.position,
                nextWaypoint.transform.position,
                Time.deltaTime * patrolSpeed);
        }
        faceDir = (nextWaypoint.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private IEnumerator PauseThenStart()
    {
        yield return null;
        startingMoving = true;       
    }
}
