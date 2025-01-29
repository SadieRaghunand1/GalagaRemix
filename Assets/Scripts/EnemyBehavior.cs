using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public EnemyData enemyData;
    private GameManager gameManager;
    [SerializeField] float health;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void OnHit(float _damage)
    {
        health -= _damage;
        if(health <= 0)
        {
            gameManager.score += enemyData.scoreWhenDead;
            Destroy(gameObject);
        }
    }
}
