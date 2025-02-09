using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    private GameManager gameManager;
    private Pause pause;
    protected EnemyParentController controller;
    public float health;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int stage;

    [Header("Shooting - Stage 1 + all")]
    [SerializeField] protected GameObject[] launchPos;
    [SerializeField] protected GameObject projectile;
    [SerializeField] private GameObject[] destPosS1;
    [SerializeField] private GameObject[] nexPhase;
    private float minShootTime = 1f;
    private float maxShootTime = 3f;

    //Stage 2
    [SerializeField] protected GameObject likeEnemiesDead;

    private void OnEnable()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        pause = FindAnyObjectByType<Pause>();

        controller = FindAnyObjectByType<EnemyParentController>();

        if(stage != 3)
        {
            StartCoroutine(TimeShoot());
            controller.enemies.Add(this.gameObject.GetComponent<EnemyBehavior>());
        }
        
    }

    /// <summary>
    /// Deducts health when enemy is hit by player's bullet
    /// </summary>
    public void OnHit(float _damage)
    {
        health -= _damage;
        if (health <= 0)
        {
            gameManager.ChangeScore(enemyData.scoreWhenDead, scoreText);

            if(stage == 1)
            {
                for (int i = 0; i < nexPhase.Length; i++)
                {
                    nexPhase[i].SetActive(true);
                }
            }

            else if(stage == 2)
            {
                //Debug.Log("Stage 2 hit");
                if(!likeEnemiesDead.active)
                {
                    Debug.Log("Stage 2 hit");
                    for (int i = 0; i < nexPhase.Length; i++)
                    {
                        nexPhase[i].SetActive(true);
                    }
                }
            }
            
            this.gameObject.SetActive(false);
        }
    }//END OnHit()

    virtual protected void Shoot()
    {
        if(!pause.isPause)
        {
            for (int i = 0; i < launchPos.Length; i++)
            {
                Stage1BossProjectile _p = Instantiate(projectile, launchPos[i].transform.position + new Vector3(0, -1, 0), launchPos[i].transform.rotation).GetComponent<Stage1BossProjectile>();

                _p.destPosObj = destPosS1[i];

            }

            StartCoroutine(TimeShoot());
        }
        
       
    }


    protected IEnumerator TimeShoot()
    {
        float _shootTime = Random.Range(minShootTime, maxShootTime);
        yield return new WaitForSeconds(_shootTime);

        Shoot();
    }


    

}
