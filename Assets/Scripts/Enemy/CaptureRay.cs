using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureRay : Projectile
{
    public StealerEnemyBehavior shooter;

   

    protected override void HitEnemy(Collision2D _collision, int _layer)
    {
        if(_collision.gameObject.layer == 8)
        {
            //shooter.StealShip();
            shooter.HitShipSteal();
            Destroy(this.gameObject);
        }
    }
}
