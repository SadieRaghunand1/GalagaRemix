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
        if(destPosObj != null)
        {
            rb.position = Vector2.MoveTowards(transform.position, destPosObj.transform.position, speed * Time.deltaTime);
        }
        else
        {
            rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + new Vector2(-5, -5) * speed * Time.fixedDeltaTime);
        }
        
    }
}
