using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyShaman : EnemyShooterBase{

    List<EnemyBase> enemies = new List<EnemyBase>();
    EnemyBase closest;

    protected override void Start()
    {
        determineClosest();
        base.Start();
    }

    protected override void Update()
    {
        determineClosest();
        base.Update();
    }

    public override void ExecuteChaseState()
    {
        if (closest != null)
        {
            enemyMovement.MoveTo(closest.transform, true);
        }
    }

    public override void ExecuteFireState()
    {
        PreFire();
        if (fireTimer <= Mathf.Epsilon)
        {
            var fireDir = (closest.transform.position - transform.position).normalized;
            GameObject bulletInstance = Instantiate(bulletPrefab, firingOrigin.position, firingOrigin.rotation);
            float angle = Mathf.Atan2(fireDir.y, fireDir.x) * Mathf.Rad2Deg - 90f;
            Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();

            bulletInstance.layer = closest.gameObject.layer;
            string name = closest.GetComponent<SpriteRenderer>().sortingLayerName;
            bulletInstance.GetComponent<SpriteRenderer>().sortingLayerName = name;
            SpriteRenderer[] srs = bulletInstance.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in srs)
            {
                sr.sortingLayerName = name;
            }
            rb.AddForce(fireDir * bulletForce, ForceMode2D.Impulse);
            rb.rotation = angle;
            fireTimer = fireInterval;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    private void determineClosest()
    {
        enemies.Clear();
        foreach(EnemyBase enemy in FindObjectsOfType<EnemyBase>()){
            if (enemy.toString() != "EnemyShaman"){
                enemies.Add(enemy);
            }
        }
        float closestDist = 1000000.0f;
        foreach(EnemyBase enemy in enemies){
            var dist = Vector3.Distance(enemy.transform.position, transform.position);
            if (dist < closestDist)
            {
                closest = enemy;
                closestDist = dist;
            }
        }
    }

    public GameObject GetClosestEnemy()
    {
        return closest.gameObject;
    }

    public override string toString(){
        return "EnemyShaman";
    }

}   
