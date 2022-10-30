using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShaman : EnemyShooterBase{

    protected override void Start()
    {
        speedMultiplier = 1.50f;
        attackRange = 2.5f;
        fireInterval = 1.0f;
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override string toString(){
        return "EnemyShaman";
    }

}   

