using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealerEnemyBehavior : EnemyBehavior
{
    [Header("Ship stealing")]
    [SerializeField] private Animator rayAnimator;
    [SerializeField] private GameObject rayObj;
    public GameObject playerCopy;
    [SerializeField] private GameObject player;
    private PlayerMovement playerScript;
    public Sprite doublePlayer;
    //[SerializeField] private GameObject capturedPlayer;
    [SerializeField] private int stealMinTime;
    [SerializeField] private int stealMaxTime;
    [SerializeField] private Transform capturePos;
    private bool shipStolen = false;
    private Rigidbody2D playerCopyRB;

    private void Awake()
    {
        base.Start();
        player = FindAnyObjectByType<PlayerMovement>().gameObject;
        playerScript = player.GetComponent<PlayerMovement>();
        playerCopy = player.GetComponentInChildren<PlayerCopyID>().gameObject;
        playerCopyRB = playerCopy.GetComponent<Rigidbody2D>();
        //Test
        StartCoroutine(TimeStealShip());
    }

    private void Update()
    {
        base.Update();

        if(shipStolen)
        {
            Debug.Log("Ship stolen");
            playerCopyRB.position = Vector2.MoveTowards(playerCopyRB.position, transform.position + new Vector3(0, yOffset, 0), (speed * 2) * Time.deltaTime); 
           // playerCopy.transform.position = capturePos.position;
        }
    }

    public void HitShipSteal()
    {
        
        playerCopy.transform.parent = null;
        playerCopy.transform.parent = transform;
        playerCopy.GetComponent<SpriteRenderer>().enabled = true;
        shipStolen = true;
        playerCopy.transform.position = capturePos.position;
    }
    
    public void StealShip()
    {
        Debug.Log("Ship stolen call");
        //play ray animation or something
        GameObject _ray = Instantiate(projectile, new Vector2(transform.position.x, transform.position.y + yOffset), projectile.transform.rotation);
        CaptureRay _rayScript = _ray.GetComponent<CaptureRay>();

        _rayScript.shooter = this;
        //playerCopy.transform.position = capturePos.position;
        StartCoroutine(TimeStealShip());
    }

    public void LoseShip()
    {
        if(shipStolen)
        {
            shipStolen = false;
            playerCopy.GetComponent<SpriteRenderer>().enabled = false;
            playerCopy.transform.position = player.transform.position;
            playerCopy.transform.parent = null;
            playerCopy.transform.parent = player.transform;
            playerScript.BecomeDouble();
            
        }
        //gameManager.enemiesKilled++;
        Destroy(this.gameObject);
    }


    IEnumerator TimeStealShip()
    {
        int _waitTime = Random.Range(stealMaxTime, stealMinTime);
        yield return new WaitForSeconds(_waitTime);
        StealShip();

    }
}
