using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cratePrefab = null;

    private bool spawned = false;

    private void OnEnable()
    {
        BossHealth.onProtect += Spawn;
    }

    private void OnDisable()
    {
        BossHealth.onProtect -= Spawn;
    }

    private void Spawn()
    {
        if (!spawned)
        {
            spawned = true;
            Instantiate(cratePrefab, transform.position, Quaternion.identity);
        }
    }
}
