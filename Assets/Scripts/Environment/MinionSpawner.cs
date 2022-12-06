using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject minion = null;
    [SerializeField] private int numMinions = 5;

    private bool spawned = false;

    private void OnEnable()
    {
        BossHealth.onSpawnMinion += Spawn;
    }

    private void OnDisable()
    {
        BossHealth.onSpawnMinion -= Spawn;
    }

    private void Spawn()
    {
        StartCoroutine(IntervalSpawn());
    } 
    
    private IEnumerator IntervalSpawn()
    {
        if (!spawned)
        {
            spawned = true;
            for (int i = 0; i < numMinions; ++i)
            {
                yield return new WaitForSeconds(1);
                Instantiate(minion, transform.position, Quaternion.identity);
            }
        }
    }
}
