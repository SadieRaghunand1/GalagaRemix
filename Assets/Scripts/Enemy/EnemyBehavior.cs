using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehavior : MonoBehaviour
{
    public enum State
    {
        ENTERING,
        PATROL,
        DIVING,
        STEALING,
        WAIT

    }



    public EnemyData enemyData;
    public float speed;
    protected GameManager gameManager;
    private Pause pause;
    [SerializeField] float health;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected float yOffset;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField] private TextMeshProUGUI scoreText;
    public State state;

    [Header("Movement - entering")]
    public GameObject[] waypoints = new GameObject[23];
    private int waypointIndex = 0;
    [SerializeField] private Rigidbody2D rb;
    private EnemyParentController controller;

    [Header("Movement - Patrol")]
    public GameObject patrolPoint;

    [Header("Movement - Diving")]
    public GameObject[] divingPathPoints;
    private int currentDiveIndex = 0;
    [SerializeField] int minTimeDive;
    [SerializeField] int maxTimeDive;

    public void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        pause = FindFirstObjectByType<Pause>();
        controller = FindFirstObjectByType<EnemyParentController>();
        
        //transform.position = waypoints[0].transform.position;

        StartCoroutine(TimeDive());
    }

     public void Update()
    {
        MoveBasedOnState();
        
    }


    

    /// <summary>
    /// Switches movement patterns based on state
    /// </summary>
    public void MoveBasedOnState()
    {
        //get speed from enemy data
        if (state == State.ENTERING) 
        {
            EnteringMovement();
        }
        else if (state == State.PATROL)
        {
            Patrol();
        }
        else if(state == State.DIVING)
        {
            Dive();
        }


    } //END MoveBasedOnState()


    /// <summary>
    /// Controls enemy movement when in entering state
    /// </summary>
    public void EnteringMovement()
    {
        if (waypointIndex <= waypoints.Length - 1)
        {
            //Debug.Log("Waypoint index = " + waypointIndex);

            //Moves enemy to next waypoint
            rb.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, speed * Time.deltaTime);

            //Checks if is in waypoint position
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                //Debug.Log("Reached waypoint");
                //Debug.Log("Destination reached");
                if (waypointIndex == waypoints.Length - 1)
                {
                    state = State.PATROL;
                    return;
                }
                    waypointIndex += 1;
            }
        }
    } //END EnteringMovement()


    /// <summary>
    /// Controls enemy when in patrol state
    /// </summary>
    private void Patrol()
    {
        //If enemy just got into patrol state and is not in position yet
        if(rb.position != new Vector2(patrolPoint.transform.position.x, patrolPoint.transform.position.y))
        {
            rb.position = Vector2.MoveTowards(transform.position, patrolPoint.transform.position, speed * Time.deltaTime);
        }
        //If enemy is in patrol position, follows grid side to side pattern
        else if(rb.position == new Vector2(patrolPoint.transform.position.x, patrolPoint.transform.position.y))
        {
            //Debug.Log("In position to patrol");
            transform.parent = patrolPoint.transform;
        }
        
    } //END Patrol()

    /// <summary>
    /// Controls behavior in diving state
    /// </summary>
    private void Dive()
    {
        transform.parent = null;
        if (currentDiveIndex <= divingPathPoints.Length - 1)
        {
            //Debug.Log("Waypoint index = " + currentDiveIndex);
            //Moves enemy to next waypoint
            rb.position = Vector2.MoveTowards(transform.position, divingPathPoints[currentDiveIndex].transform.position, speed * Time.deltaTime);

            //Checks if is in waypoint position
            if (transform.position == divingPathPoints[currentDiveIndex].transform.position)
            {
                //Debug.Log("Destination reached diving");
                if (currentDiveIndex == divingPathPoints.Length - 1)
                {
                    state = State.PATROL;
                    currentDiveIndex = 0;
                    return;
                }
                currentDiveIndex += 1;
            }
        }

    }//END Dive()

    /// <summary>
    /// Deducts health when enemy is hit by player's bullet
    /// </summary>
    public void OnHit(float _damage)
    {
        health -= _damage;
        if(health <= 0)
        {
            gameManager.ChangeScore(enemyData.scoreWhenDead, scoreText);
            gameManager.enemiesKilled++;
            Debug.Log("Enemies killed " + gameManager.enemiesKilled);
            Destroy(gameObject);
        }
    }//END OnHit()

    /// <summary>
    /// Enemy shoots bullet
    /// </summary>
    private void Shoot()
    {
        if(!pause.isPause)
        Instantiate(projectile, new Vector2(transform.position.x, transform.position.y + yOffset), projectile.transform.rotation);
    } //END Shoot()


    /// <summary>
    /// When in diving mode, randomizes shooting rate for enemies
    /// </summary>
    private IEnumerator TimeShoot() //Start when starting diving state
    {
        float _seconds = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(_seconds);

        if(state == State.DIVING)
        {
            Shoot();
            StartCoroutine(TimeShoot());
        }
    } //END TimeShoot()


    public IEnumerator DelayEntrance(int _delay)
    {
        
        speed = 0;
        
        yield return new WaitForSeconds(_delay);
        speed = enemyData.speed;
    }

    private IEnumerator TimeDive()
    {
        int _waitTime = Random.Range(minTimeDive, maxTimeDive);
        yield return new WaitForSeconds(_waitTime);
        state = State.DIVING;
        StartCoroutine(TimeShoot());
    }
}
