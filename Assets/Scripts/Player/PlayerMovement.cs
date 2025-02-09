using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerLives;

public class PlayerMovement : MonoBehaviour
{

    [System.Serializable]
    public struct PlayerSprites
    {
        public Sprite singleSprite;
        public Sprite doubleSprite;
    }



    private GameManager gameManager;
    private PlayerController1 playerControls;
    float horizontal;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    [SerializeField] GameObject projectileDefault;
    [SerializeField] GameObject projectileQuick;
    [SerializeField] private float yOffset;

    [SerializeField] float shootDefaultCooldown;
    private bool canShootDefault = true;
    private SpriteRenderer spriteRenderer;
    public bool doubleShip = false;
    int countShotsN;
    int countShotsQ;
    public Sprite doublePlayer;
    public Sprite normalPlayer;

    private Pause pause;

    public PlayerSprites[] spriteOptions;
    public PlayerSprites spriteChosen;
    private StealerEnemyBehavior stealEnemies; 

    // Start is called before the first frame update
    void Awake()
    {
        InitValues();
        //Testing
        //Instantiate(projectile);
    }



    private void FixedUpdate()
    {
        Move();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    void InitValues()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        //Attach player controls for movement
        playerControls = new PlayerController1();

        playerControls.Player.Movement.performed += ctx => horizontal = ctx.ReadValue<float>();
        playerControls.Player.Movement.canceled += _ => horizontal = 0;


        //Append Shoot() to shoot action depending on chosen ship
        if (gameManager.defaultShip)
        {
            Debug.Log("Default ship");
            playerControls.Player.Shoot.started += _ => Shoot();
        }
        else if (!gameManager.defaultShip)
        {
            Debug.Log("Not default ship");
            playerControls.Player.Shoot.started += _ => ShootQuick();
        }



        doubleShip = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        pause = FindAnyObjectByType<Pause>();

        ChangePlayerSprites();
    } //End InitValues()


    private void ChangePlayerSprites()
    {
        if (gameManager.defaultShip)
        {
            spriteChosen = spriteOptions[0];
        }
        else if (!gameManager.defaultShip)
        {
            spriteChosen = spriteOptions[1];
        }

        spriteRenderer.sprite = spriteChosen.singleSprite;
        normalPlayer = spriteChosen.singleSprite;
        doublePlayer = spriteChosen.doubleSprite;

        //Get player copy 
        stealEnemies = FindFirstObjectByType<StealerEnemyBehavior>();
        stealEnemies.playerCopy.GetComponent<SpriteRenderer>().sprite = spriteChosen.singleSprite;
        //allStealEnemies = FindObjectsOfType<StealerEnemyBehavior>();
        /*for(int i = 0; i < allStealEnemies.Length; i++)
        {
            allStealEnemies[i].playerCopy.GetComponent<SpriteRenderer>().sprite = spriteChosen.singleSprite;
        }*/

    }




    /// <summary>
    /// Get velocityon x axis and use for movement
    /// </summary>
    private void Move()
    {
        //Debug.Log("Horizontal " + horizontal);
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

    } //END Walk()

    /// <summary>
    /// Shoot projectile on button press, works for default projectile
    /// </summary>
    private void Shoot()
    {
        
        if (canShootDefault && !pause.isPause) 
        {
            if(!doubleShip)
            {
                Debug.Log("Shoot as single");
                Instantiate(projectileDefault, new Vector2(transform.position.x, transform.position.y + yOffset), projectileDefault.transform.rotation);
            }
            
            if(doubleShip && countShotsN == 0)
            {
                Debug.Log("Shoot as double");
                Instantiate(projectileDefault, new Vector2(transform.position.x, transform.position.y + yOffset), projectileDefault.transform.rotation);
                StartCoroutine(ShootCooldown(0.2f));
            }

            StartCoroutine(ShootCooldown());
        }
        
        
    } //END Shoot

    /// <summary>
    /// Quick shooting
    /// </summary>
    private void ShootQuick()
    {
        if(!pause.isPause)
        {
            Instantiate(projectileQuick, new Vector2(transform.position.x, transform.position.y + yOffset), projectileQuick.transform.rotation);

            if (doubleShip && countShotsQ == 0)
            {
                StartCoroutine(ShootDoubleQuick());

            }
        }
        
    } //END ShootQuick()

    /// <summary>
    /// Enter double ship state when player recaptures ship
    /// </summary>
    public void BecomeDouble()
    {
        spriteRenderer.sprite = doublePlayer;
        doubleShip = true;
        
    } //END BecomeDouble

    /// <summary>
    /// Go back to single ship state when hit as a double ship
    /// </summary>
    public void BecomeSingle()
    {
        doubleShip = false;
        spriteRenderer.sprite = normalPlayer;
    } //END BecomeSingle()

    /// <summary>
    /// Cooldown for normal bullets, also handles double shooting normal bullets for double ship state
    /// </summary>
    private IEnumerator ShootCooldown(float _timeShoot = 0)
    {
        canShootDefault = false;
        
        //Normal, not double ship double shoot
        if (_timeShoot == 0)
        {
            
            yield return new WaitForSeconds(shootDefaultCooldown);
            countShotsN = 0;
        }
        //If double ship has to double shoot
        else if(_timeShoot != 0)
        {
            yield return new WaitForSeconds(_timeShoot);
            canShootDefault = true;
            Shoot();
            countShotsN++;
        }
        
        canShootDefault = true;
    } //END ShootCooldow()


    private IEnumerator ShootDoubleQuick()
    {
        yield return new WaitForSeconds(0.1f);

        if(countShotsQ == 0)
        {
            ShootQuick();
            countShotsQ++;
        }
        else
        {
            countShotsQ = 0;
        }
        
    }
}
