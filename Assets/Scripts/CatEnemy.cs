using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEnemy : Enemy
{
    // Update is called once per frame
    override protected void Update()
    {
        speed = 6f;
        damage = 10f;
        base.Update();
    }
}
