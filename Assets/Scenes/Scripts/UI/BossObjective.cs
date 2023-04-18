using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossObjective : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text = null;

    private void OnEnable()
    {
        BossHealth.onVulnerable += Vulnerable;
        BossHealth.onSpawnMinion += Spawn;
        BossHealth.onProtect += Protect;
    }

    private void OnDisable()
    {
        BossHealth.onVulnerable -= Vulnerable;
        BossHealth.onSpawnMinion -= Spawn;
        BossHealth.onProtect -= Protect;
    }

    private void Vulnerable()
    {
        text.text = "Boss is now vulnerable! Shoot it";
    }

    private void Spawn()
    {
        text.text = "Shoot at spike balls to damage the boss!";
    }

    private void Protect()
    {
        text.text = "Boss is now under protection mode. Destroy barrels to find a rocket launcher!";
    }
}
