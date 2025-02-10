using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureRay : Projectile
{
    public StealerEnemyBehavior shooter;

   

    protected override void HitEnemy(Collider2D _collision, int _layer)
    {
        if(_collision.gameObject.layer == 8)
        {

            //shooter.StealShip();
            
            if(shooter != null)
            {
                shooter.HitShipSteal();
            }
            
            
            
            if(!gameManager.cheatmode)
            {
                PlayerLives _playerLives = _collision.gameObject.GetComponent<PlayerLives>();
                _playerLives.GetHit();
            }
            else
            {
                PlayerMovement _playerMovement = _collision.gameObject.GetComponent<PlayerMovement>();
                _playerMovement.BecomeSingle();
            }
            
            Destroy(this.gameObject);
        }
    }
}
