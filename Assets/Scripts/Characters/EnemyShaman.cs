using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShaman : EnemyBase{

    protected EnemyShooterBase enemy;
    protected override void Start()
    {
        speedMultiplier = 1.50f;
        attackRange = 2.5f;
        fireInterval = 1.0f;
        enemy = FindObjectOfType<EnemyShooterBase>();
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FollowPLayerIfSeen()
    {   
        Debug.Log(enemy);
        Debug.Log(Vector2.Distance(enemy.transform.position, this.transform.position));
        //If not near enemy
    
        // else
            // stay near enemy
    }

    public override string toString(){
        return "EnemyShaman";
    }

}   

