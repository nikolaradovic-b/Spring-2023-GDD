using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    [SerializeField] private GameObject weaponsContainer = null;
    [SerializeField] private GameObject ammosContainer = null;

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
        if (!spawned)
        {
            spawned = true;
            weaponsContainer.SetActive(true);
            ammosContainer.SetActive(true);
        }
    }
}
