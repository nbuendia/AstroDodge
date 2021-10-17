using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworksEnemy : Enemy
{
    // Update is called once per frame
    override protected void Update()
    {
        speed = 7f;
        damage = 15f;
        base.Update();
    }
}
