using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailManEnemy : Enemy
{
    // Update is called once per frame
    override protected void Update()
    {
        speed = 5f;
        damage = 5f;
        base.Update();
    }
}
