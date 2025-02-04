using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public enum State
    {
        ENTERING,
        PATROL,
        DIVING,
        WAIT

    }



    public EnemyData enemyData;
    private GameManager gameManager;
    [SerializeField] float health;
    [SerializeField] GameObject projectile;
    [SerializeField] private float yOffset;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;

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
    private int currentDiveIndex;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        controller = FindFirstObjectByType<EnemyParentController>();
        
        //Test
        //StartCoroutine(TimeShoot());
        transform.position = waypoints[0].transform.position;
    }

    private void Update()
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


    } //END MoveBasedOnState()


    /// <summary>
    /// Controls enemy movement when in entering state
    /// </summary>
    public void EnteringMovement()
    {
        if (waypointIndex <= waypoints.Length - 1)
        {
            Debug.Log("Waypoint index = " + waypointIndex);

            //Moves enemy to next waypoint
            rb.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, enemyData.speed * Time.deltaTime);

            //Checks if is in waypoint position
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                Debug.Log("Destination reached");
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
            rb.position = Vector2.MoveTowards(transform.position, patrolPoint.transform.position, enemyData.speed * Time.deltaTime);
        }
        //If enemy is in patrol position, follows grid side to side pattern
        else if(rb.position == new Vector2(patrolPoint.transform.position.x, patrolPoint.transform.position.y))
        {
            Debug.Log("In position to patrol");
            transform.parent = patrolPoint.transform;
        }
        
    } //END Patrol()

    /// <summary>
    /// Controls behavior in diving state
    /// </summary>
    private void Dive()
    {
        if (currentDiveIndex <= divingPathPoints.Length - 1)
        {
            Debug.Log("Waypoint index = " + waypointIndex);

            //Moves enemy to next waypoint
            rb.position = Vector2.MoveTowards(transform.position, divingPathPoints[currentDiveIndex].transform.position, enemyData.speed * Time.deltaTime);

            //Checks if is in waypoint position
            if (transform.position == divingPathPoints[currentDiveIndex].transform.position)
            {
                Debug.Log("Destination reached diving");
                //Do sonething
                waypointIndex += 1;
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
            gameManager.score += enemyData.scoreWhenDead;
            Destroy(gameObject);
        }
    }//END OnHit()

    /// <summary>
    /// Enemy shoots bullet
    /// </summary>
    private void Shoot()
    {
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
        }
    } //END TimeShoot()
}
