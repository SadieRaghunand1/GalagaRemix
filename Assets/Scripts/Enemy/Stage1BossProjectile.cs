using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1BossProjectile : Projectile
{

    public GameObject destPosObj;


    private void FixedUpdate()
    {
        MoveForward();
    }

    protected override void MoveForward()
    {
        rb.position = Vector2.MoveTowards(transform.position, destPosObj.transform.position, speed * Time.deltaTime);
    }
}
