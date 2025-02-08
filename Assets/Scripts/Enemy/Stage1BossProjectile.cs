using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1BossProjectile : Projectile
{

    public GameObject destPosObj;
    private PlayerMovement player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        MoveForward();
    }

    protected override void MoveForward()
    {
        if(destPosObj != null) //phase 1 + 2
        {
            rb.position = Vector2.MoveTowards(transform.position, destPosObj.transform.position, speed * Time.fixedDeltaTime);
        }
        else //phase 3
        {
            rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + new Vector2(0,-1) * speed * Time.fixedDeltaTime);
            
        }
        
    }
}
