using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private AudioSource audioSource;
    private GameManager gameManager;
    private Pause pause;
    protected EnemyParentController controller;
    public float health;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int stage;

    [Header("Stage 1 + all")]
    [SerializeField] protected GameObject[] launchPos;
    [SerializeField] protected GameObject projectile;
    [SerializeField] private GameObject[] destPosS1;
    [SerializeField] private GameObject[] nexPhase;
    private float minShootTime = 1f;
    private float maxShootTime = 3f;
    [SerializeField] private ParticleSystem deathParticle;

    //Stage 2
    [SerializeField] protected GameObject likeEnemiesDead;

    [Header("Stage 3")]
    [SerializeField] private S3Boss[] phase3Script;
    public int countDead;
    [SerializeField] private EnemyParentController enemyController;

    private void OnEnable()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        pause = FindAnyObjectByType<Pause>();
        //phase3Script = FindObjectsOfType<S3Boss>();
        StartCoroutine(WaitToLoadS3BossList());
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
        Debug.Log("Get hit");
        health -= _damage;
        if (health <= 0)
        {
            gameManager.ChangeScore(enemyData.scoreWhenDead, scoreText);
            deathParticle.gameObject.transform.position = this.gameObject.transform.position;
            deathParticle.Play();
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
                    enemyController.AssignPatrolPoints();
                }
            }

            else if(stage == 3)
            {
                //Debug.Log("Stage 3 hit");
                
                for(int i = 0; i < phase3Script.Length; i++)
                {
                    Debug.Log("in for loop " + phase3Script[i].countDead);
                    if (phase3Script[i] != null)
                    {
                        Debug.Log("Not null hit");
                        phase3Script[i].countDead++;
                    }
                    else
                    {
                        Debug.Log("Counts as skip");
                        continue;
                    }
                    
                }

                if (countDead == 4)
                {
                    SceneManager.LoadScene(4);
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
                audioSource.Play();
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

    protected IEnumerator WaitToLoadS3BossList()
    {
        yield return new WaitForEndOfFrame();
        phase3Script = FindObjectsOfType<S3Boss>();
    }
    

}
