using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParentController : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;
    public List<EnemyBehavior> enemies;
   

    [Header("Waypoints")]
    [SerializeField] private GameObject[] bottomLeftEntrance;
    [SerializeField] private GameObject[] topLeftEntrance;
    [SerializeField] private GameObject[] bottomRightEntrance;
    [SerializeField] private GameObject[] topRightEntrance;
    [SerializeField] private GameObject[] patrolPoints;
    [SerializeField] private GameObject[] divePath1;
    [SerializeField] private GameObject[] divePath2;
    [SerializeField] private GameObject[] divePath3;
    


    private void Start()
    {
        //SpawnEnemies();
        
        AssignPatrolPoints();
    }



    void AssignWaypointSet()
    {
        int _n = 0;
        for(int i = 0; i < enemies.Count; i++)
        {
            if(_n == 4)
            {
                _n = 0;
            }

            switch(_n)
            {
                case 0:
                    enemies[i].waypoints = bottomLeftEntrance; break;
                case 1:
                    enemies[i].waypoints = topLeftEntrance; break;
                case 2:
                    enemies[i].waypoints = bottomRightEntrance; break;
                case 3:
                    enemies[i].waypoints = topRightEntrance; break;
            }

            _n++;

            //enemies[i].waypoints = 
          
        }
    }

    void AssignPatrolPoints()
    {
        //Find all enemies
        EnemyBehavior[] _enemies = FindObjectsByType<EnemyBehavior>(FindObjectsSortMode.None);
        //Delete previous enemies for new wave
        enemies.Clear();

        //Add enemies in this wave to list
        for (int i = 0; i < _enemies.Length; i++)
        {
            enemies.Add(_enemies[i]);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].patrolPoint = patrolPoints[i];
        }

        AssignWaypointSet();
    }
}
