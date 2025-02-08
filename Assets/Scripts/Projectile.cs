using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float speed;
    public ProjectileData projectileData;
    [SerializeField] int layer;
    [SerializeField] Vector2 direction;

    private void FixedUpdate()
    {
        MoveForward();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HitEnemy(collision, layer);
    }

    virtual protected void HitEnemy(Collision2D _collision, int _layer)
    {
        
        if(_collision.gameObject.layer == _layer)
        {
            

            if(_layer == 6) //Hits enemy, player shoots
            {
                EnemyBehavior _enemyHit = _collision.gameObject.GetComponent<EnemyBehavior>();
                if(_enemyHit == null)
                {
                    BossBehavior _boss = _collision.gameObject.GetComponent<BossBehavior>();
                    _boss.OnHit(projectileData.damageDealt);
                }
                else
                {
                    _enemyHit.OnHit(projectileData.damageDealt);

                    if (_enemyHit.gameObject.GetComponent<StealerEnemyBehavior>() != null)
                    {
                        StealerEnemyBehavior _stealer = _enemyHit.gameObject.GetComponent<StealerEnemyBehavior>();
                        _stealer.LoseShip();

                    }
                }
                
                Destroy(this.gameObject);

                

                
            }
            else if(_layer == 8) //Hits player, enemy shoots
            {
                PlayerLives _playerLives = _collision.gameObject.GetComponent<PlayerLives>();
                _playerLives.GetHit();
               
                Destroy(this.gameObject);
            }
            

            
        }
    }


    virtual protected void MoveForward()
    {
        
        rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + direction * speed * Time.fixedDeltaTime);
        
    }
}
