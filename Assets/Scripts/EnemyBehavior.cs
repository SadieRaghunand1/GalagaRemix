using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public EnemyData enemyData;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void OnHit(float _damage)
    {
        enemyData.health -= _damage;
        if(enemyData.health <= 0)
        {
            gameManager.score += enemyData.scoreWhenDead;
            Destroy(gameObject);
        }
    }
}
