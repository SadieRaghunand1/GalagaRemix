using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParentController : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;
    [SerializeField] private EnemyBehavior[] enemyChildren;
    [SerializeField] private GameObject[] spawnPos;
    [SerializeField] private Animator animator;

    private void Start()
    {
        SpawnEnemies();
    }


    public void SpawnEnemies()
    {
        for(int i = 0; i < spawnPos.Length; i++)
        {
            //For testing, just spawning in the one enemy
            enemyChildren[i] = Instantiate(enemiesToSpawn[0], spawnPos[i].transform.position, enemiesToSpawn[0].transform.rotation).GetComponent<EnemyBehavior>();
            enemyChildren[i].transform.parent = spawnPos[i].transform;
        }
    }
}
