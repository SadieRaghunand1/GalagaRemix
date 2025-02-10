using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyParentController : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;
    public List<EnemyBehavior> enemies;
    public int enemiesKilled = 0;
    private GameManager gameManager;
    private float timer = 40;

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
    [SerializeField] private GameObject[] bossPatrolPts;

    [Header("Switch to Boss")]
    [SerializeField] private GameObject bossParent;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timerText;

    private void Start()
    {
        //SpawnEnemies();
        gameManager = FindAnyObjectByType<GameManager>();
        AssignPatrolPoints();
    }

    private void Update()
    {
        Timer();
    }

    void AssignWaypointSet()
    {

        if (enemies[0].gameObject.GetComponent<S3Boss>() != null)
        {
            Debug.Log("Assigning pts to boss");
        }


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
                    //Debug.Log("In switch for assign");
                    enemies[i].waypoints = bottomLeftEntrance;
                    enemies[i].gameObject.transform.position = bottomLeftEntrance[0].transform.position;
                    break;
                case 1:
                    //Debug.Log("In switch for assign");
                    enemies[i].waypoints = topLeftEntrance;
                    enemies[i].gameObject.transform.position = topLeftEntrance[0].transform.position;
                    break;
                case 2:
                    //Debug.Log("In switch for assign");
                    enemies[i].waypoints = bottomRightEntrance;
                    enemies[i].gameObject.transform.position = bottomRightEntrance[0].transform.position;
                    break;
                case 3:
                    //Debug.Log("In switch for assign");
                    enemies[i].waypoints = topRightEntrance;
                    enemies[i].gameObject.transform.position = topRightEntrance[0].transform.position;
                    break;
                default:
                    //Debug.Log("Default");
                    break;
            }

            StartCoroutine(enemies[i].DelayEntrance(_delay));
            _n++;

            
          
        }

        //Assign dive waypoints
      
            for (int i = 0; i < enemies.Count; i++)
            {
                if (_m == 5)
                {
                    _m = 0;
                }


                switch (_m)
                {
                    case 0:
                        if (enemies[i] != null)
                            enemies[i].divingPathPoints = divePath1;
                        break;
                    case 1:
                        if (enemies[i] != null)
                            enemies[i].divingPathPoints = divePath2;
                        break;
                    case 2:
                        if (enemies[i] != null)
                            enemies[i].divingPathPoints = divePath3;
                        break;
                    case 3:
                        if (enemies[i] != null)
                            enemies[i].divingPathPoints = divePath4;
                        break;
                    case 4:
                        if (enemies[i] != null)
                            enemies[i].divingPathPoints = divePath5;
                        break;
                }

                _m++;
            }
        




    }

    public void AssignPatrolPoints()
    {
        //Delete previous enemies for new wave
        enemies.Clear();
        //Find all enemies
        EnemyBehavior[] _enemies = FindObjectsByType<EnemyBehavior>(FindObjectsSortMode.None);
        
        //Add enemies in this wave to list
        for (int i = 0; i < _enemies.Length; i++)
        {
            Debug.Log("Enemies should be added");
            enemies.Add(_enemies[i]);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].gameObject.tag == "BossEnemyS3")
            {
                Debug.Log("In assign patrol pts boss " + enemies[i].gameObject.name);
                enemies[i].patrolPoint = bossPatrolPts[i];
            }
            else
            {
                enemies[i].patrolPoint = patrolPoints[i];
            }
            
        }

        

        AssignWaypointSet();
    }


    
    private void Timer()
    {
        timer -= Time.deltaTime;
        if(gameManager.stage != 2)
        {
            timerText.text = "Wave 1: " + timer;
        }
        else if (gameManager.stage == 2)
        {
            timerText.text = "Wave 2: " + timer;
        }
        

        if ((timer <= 0 || gameManager.enemiesKilled == 31) && gameManager.stage == 1)
        {
            Debug.Log("Switch");
            //timerText.enabled = false;
            SwitchStages();
        }

        if(timer <= 0 && gameManager.stage == 2)
        {
            SceneManager.LoadScene(5);
        }
    }

    public void SwitchStages()
    {
        //gameManager.stage = 2;

        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null && gameManager.stage < 2)
            {
                Debug.Log("Switch stage deactivate");
                enemies[i].gameObject.SetActive(false);
            }
            else
            {
                continue;
            }
            
        }


        enemies.Clear();

        bossParent.SetActive(true);
        gameManager.stage = 2;

        timer = 30;
    }
}
