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

    public GameObject GetPlayer()
    {
        if (player == null)
        {
            return null;
        }
        return player.gameObject;
    }

    public bool CanSeePlayer(GameObject obj, float range)
    {
        if (player == null)
        {
            return false;
        }
        if (player.gameObject.layer == obj.layer || player.gameObject.layer == LayerMask.NameToLayer("Player"))
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

    public bool TargetInAttackRange(GameObject from, GameObject target, float range)
    {
        if (target == null)
        {
            return false;
        }
        return target.layer == from.layer &&
            Vector2.Distance(target.transform.position, from.transform.position) <= range;
    }

    public bool CanBossAttack(Boss boss)
    {
        return boss.GetCanFire();
    }

    public bool GetBossProtect(Boss boss)
    {
        return boss.GetPhase() == BossPhase.Protection;
    }
}
