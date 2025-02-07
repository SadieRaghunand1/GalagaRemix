using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    private GameManager gameManager;
    public float health;


    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    /// <summary>
    /// Deducts health when enemy is hit by player's bullet
    /// </summary>
    public void OnHit(float _damage)
    {
        health -= _damage;
        if (health <= 0)
        {
            gameManager.score += enemyData.scoreWhenDead;
            //gameManager.enemiesKilled++;
            Destroy(gameObject);
        }
    }//END OnHit()


}
