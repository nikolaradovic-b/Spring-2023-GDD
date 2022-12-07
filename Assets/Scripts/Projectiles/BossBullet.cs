using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Bullet
{
    protected override void Start()
    {
        Destroy(gameObject, 2f);
    }
}
