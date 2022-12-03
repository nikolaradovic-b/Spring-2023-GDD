using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollingMachine : MonoBehaviour
{
    [SerializeField] private float playerProximityLimit = 3f;

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

    public bool CanSeePlayer(GameObject obj)
    {
        if (player.gameObject.layer == obj.layer)
        {
            // same layer, then check distance
            float distance = Vector2.Distance(player.transform.position, obj.transform.position);
            if (distance < playerProximityLimit)
            {
                // player in range
                return true;
            }
        }
        return false;
    }
}
