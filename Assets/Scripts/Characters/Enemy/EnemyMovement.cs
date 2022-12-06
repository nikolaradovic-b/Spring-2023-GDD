using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float patrolSpeed = 1.5f;
    [SerializeField] private float chaseMultiplier = 2f;
    [SerializeField] private float strafeMultiplier = 6f;
    [SerializeField] private float arrivalOffsetAllowed = 0.25f;
    [SerializeField] private float pauseTime = 2f;

    protected GameObject nextWaypoint;
    private Rigidbody2D rb;
    private EnemyBase enemy;
    private AIDestinationSetter destinationSetter;
    private AIPath path;

    private int currentWaypointIndex;
    private bool startingMoving = true;

    private void Start()
    {
        FindObjectOfType<GameManager>().RegisterEnemy();

        // Initialize enemy location and variables
        if (waypoints.Length > 0)
        {
            int randomIndex = Random.Range(0, waypoints.Length);
            currentWaypointIndex = randomIndex;
            GameObject initialWaypoint = waypoints[randomIndex];
            transform.position = initialWaypoint.transform.position;
            nextWaypoint = initialWaypoint;
        }
        
        rb = GetComponentInChildren<Rigidbody2D>();
        enemy = GetComponentInChildren<EnemyBase>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        path = GetComponent<AIPath>();
    }

    public void ExecutePatrolState()
    {
        if (nextWaypoint == null)
        {
            return;
        }

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
            MoveTo(nextWaypoint.transform, false);
        }
        
        AdjustFaceDirectionIfMelee(nextWaypoint.transform.position);
    }

    public void AdjustFaceDirectionIfMelee(Vector3 facePos)
    {
        var faceDir = (facePos - transform.position).normalized;
        if (enemy.toString() == "EnemyMeleeBase")
        {
            if (faceDir.x < 0)
            {
                // make lookDirection.y negative
                faceDir = new Vector2(faceDir.x, -Mathf.Abs(faceDir.y));
            }
            else
            {
                // make lookDirection.y positive
                faceDir = new Vector2(faceDir.x, Mathf.Abs(faceDir.y));
            }
            Vector3 currScale = transform.localScale;
            float scaler = currScale.y;
            rb.transform.localScale =
                Mathf.Atan2(faceDir.y, faceDir.x) > 0 ?
                new Vector3(-1.0f, 1.0f, 1.0f) * scaler : new Vector3(1.0f, 1.0f, 1.0f) * scaler;
        }
    }

    public void MoveTo(Transform position, bool chase, bool strafe=false)
    {
        destinationSetter.target = position;
        if (chase)
        {
            path.maxSpeed = patrolSpeed * chaseMultiplier;
        }
        else if (strafe)
        {
            path.maxSpeed = patrolSpeed * strafeMultiplier;
        }
        else
        {
            path.maxSpeed = patrolSpeed;
        }
        SetAnimationIfMelee();
    }

    private void SetAnimationIfMelee()
    {
        var animator = GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetInteger("AnimState", 2);
        }
    }

    public void StopMovement()
    {
        destinationSetter.target = null;
        path.maxSpeed = 0f;
    }

    private IEnumerator PauseThenStart()
    {
        yield return new WaitForSeconds(pauseTime);
        startingMoving = true;
    }
}