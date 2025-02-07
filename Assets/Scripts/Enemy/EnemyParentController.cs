using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParentController : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;
    public List<EnemyBehavior> enemies;
    public int enemiesKilled = 0;

    [Header("Waypoints")]
    [SerializeField] private GameObject[] bottomLeftEntrance;
    [SerializeField] private GameObject[] topLeftEntrance;
    [SerializeField] private GameObject[] bottomRightEntrance;
    [SerializeField] private GameObject[] topRightEntrance;
    [SerializeField] private GameObject[] patrolPoints;
    [SerializeField] private GameObject[] divePath1;
    [SerializeField] private GameObject[] divePath2;
    [SerializeField] private GameObject[] divePath3;
    [SerializeField] private GameObject[] divePath4;
    [SerializeField] private GameObject[] divePath5;
    


    private void Start()
    {
        //SpawnEnemies();
        
        AssignPatrolPoints();
    }



    void AssignWaypointSet()
    {
        int _n = 0;
        int _m = 0;
        int _delay = 0;

        //Assign entrance waypoints
        for(int i = 0; i < enemies.Count; i++)
        {
            if(_n == 4)
            {
                _n = 0;
                _delay++;
                
            }

            switch(_n)
            {
                case 0:
                    enemies[i].waypoints = bottomLeftEntrance;
                    enemies[i].gameObject.transform.position = bottomLeftEntrance[0].transform.position;
                    break;
                case 1:
                    enemies[i].waypoints = topLeftEntrance;
                    enemies[i].gameObject.transform.position = topLeftEntrance[0].transform.position;
                    break;
                case 2:
                    enemies[i].waypoints = bottomRightEntrance;
                    enemies[i].gameObject.transform.position = bottomRightEntrance[0].transform.position;
                    break;
                case 3:
                    enemies[i].waypoints = topRightEntrance;
                    enemies[i].gameObject.transform.position = topRightEntrance[0].transform.position;
                    break;
            }

            StartCoroutine(enemies[i].DelayEntrance(_delay));
            _n++;

            
          
        }

        //Assign dive waypoints
        for(int i = 0; i < enemies.Count; i++)
        {
            if(_m == 5)
            {
                _m = 0;
            }

            switch(_m)
            {
                case 0:
                    enemies[i].divingPathPoints = divePath1;
                    break;
                case 1:
                    enemies[i].divingPathPoints = divePath2;
                    break;
                case 2:
                    enemies[i].divingPathPoints = divePath3;
                    break;
                case 3:
                    enemies[i].divingPathPoints = divePath4;
                    break;
                case 4:
                    enemies[i].divingPathPoints = divePath5;
                    break;
            }

            _m++;
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
