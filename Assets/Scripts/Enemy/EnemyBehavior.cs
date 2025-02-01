using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public EnemyData enemyData;
    private GameManager gameManager;
    [SerializeField] float health;
    [SerializeField] GameObject projectile;
    [SerializeField] private float yOffset;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;

    private bool isDiving = true; //true for testing


    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        //Test
        StartCoroutine(TimeShoot());
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


    private void Shoot()
    {
        Instantiate(projectile, new Vector2(transform.position.x, transform.position.y + yOffset), projectile.transform.rotation);
    }

    private IEnumerator TimeShoot() //Start when starting diving state
    {
        float _seconds = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(_seconds);

        if(isDiving)
        {
            Shoot();
        }
    }
}
