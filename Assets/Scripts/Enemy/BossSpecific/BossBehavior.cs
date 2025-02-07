using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    private GameManager gameManager;
    public float health;

    [SerializeField] private int stage;

    [Header("Shooting - Stage 1 + all")]
    [SerializeField] protected GameObject[] launchPos;
    [SerializeField] protected GameObject projectile;
    [SerializeField] private GameObject[] destPosS1;
    private float minShootTime = 1f;
    private float maxShootTime = 3f;

    private void OnEnable()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        StartCoroutine(TimeShoot());
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
            this.gameObject.SetActive(false);
        }
    }//END OnHit()

    virtual protected void Shoot()
    {
        for(int i = 0; i < launchPos.Length; i++)
        {
            Stage1BossProjectile _p = Instantiate(projectile, launchPos[i].transform.position + new Vector3(0, -1, 0), launchPos[i].transform.rotation).GetComponent<Stage1BossProjectile>();

            _p.destPosObj = destPosS1[i];

        }

        StartCoroutine(TimeShoot());
    }


    protected IEnumerator TimeShoot()
    {
        float _shootTime = Random.Range(minShootTime, maxShootTime);
        yield return new WaitForSeconds(_shootTime);

        Shoot();
    }


    

}
