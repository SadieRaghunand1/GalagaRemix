using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    public ProjectileData projectileData;

    private void FixedUpdate()
    {
        MoveForward();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HitEnemy(collision);
    }

    void HitEnemy(Collision2D _collision)
    {
        if(_collision.gameObject.layer == 6)
        {
            EnemyBehavior _enemyHit = _collision.gameObject.GetComponent<EnemyBehavior>();
            _enemyHit.OnHit(projectileData.damageDealt);
            Destroy(this.gameObject);

            
        }
    }


    void MoveForward()
    {
        rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + Vector2.up * speed * Time.fixedDeltaTime);
        
    }
}
