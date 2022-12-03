using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollingMachine : MonoBehaviour
{
    public static PollingMachine Instance;

    private PlayerMovement player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        player = FindObjectOfType<PlayerMovement>();
    }

    public bool CanSeePlayer(GameObject obj, float range)
    {
        if (player == null)
        {
            return false;
        }
        if (player.gameObject.layer == obj.layer)
        {
            // same layer, then check distance
            float distance = Vector2.Distance(player.transform.position, obj.transform.position);
            if (distance < range)
            {
                // player in range
                return true;
            }
        }
        return false;
    }

    public bool PlayerInAttackRange(GameObject obj, float range)
    {
        if (player == null)
        {
            return false;
        }
        return player.gameObject.layer == obj.layer &&
            Vector2.Distance(player.transform.position, obj.transform.position) <= range;
    }
}
