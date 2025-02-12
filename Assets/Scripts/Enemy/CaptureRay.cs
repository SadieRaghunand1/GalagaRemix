using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureRay : Projectile
{
    public StealerEnemyBehavior shooter;

    protected void Start()
    {
        base.Start();
        StartCoroutine(DestroyBullet());
    }

    protected override void HitEnemy(Collider2D _collision, int _layer)
    {
        if(_collision.gameObject.layer == 8)
        {

            //shooter.StealShip();
            
            if(shooter != null)
            {
                shooter.HitShipSteal();
            }


            PlayerLives _playerLives = _collision.gameObject.GetComponent<PlayerLives>();
            if (!gameManager.cheatmode)
            {
                
                _playerLives.GetHit();
            }
            else
            {
                PlayerMovement _playerMovement = _collision.gameObject.GetComponent<PlayerMovement>();
                _playerMovement.BecomeSingle();
            }
            _playerLives.deathParticle.Play();
            Destroy(this.gameObject);
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
