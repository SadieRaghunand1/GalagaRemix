using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
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
    public Sprite doublePlayer;
    public Sprite normalPlayer;

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
        //Attach player controls for movement
        playerControls = new PlayerController1();

        playerControls.Player.Movement.performed += ctx => horizontal = ctx.ReadValue<float>();
        playerControls.Player.Movement.canceled += _ => horizontal = 0;


        //Append Shoot() to shoot action
        playerControls.Player.Shoot.started += _ => Shoot();

        //Append ShootQuick to shoot action
        playerControls.Player.ShootQuick.started += _ => ShootQuick();


        spriteRenderer = GetComponent<SpriteRenderer>();
    } //End InitValues()

    /// <summary>
    /// Get velocityon x axis and use for movement
    /// </summary>
    private void Move()
    {
        Debug.Log("Horizontal " + horizontal);
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

    } //END Walk()

    /// <summary>
    /// Shoot projectile on button press, works for default projectile
    /// </summary>
    private void Shoot()
    {
        if (canShootDefault) 
        {
            Instantiate(projectileDefault, new Vector2(transform.position.x, transform.position.y + yOffset), projectileDefault.transform.rotation);
            StartCoroutine(ShootCooldown());
        }
        
        
    } //END Shoot

    private void ShootQuick()
    {
        Instantiate(projectileQuick, new Vector2(transform.position.x, transform.position.y + yOffset), projectileQuick.transform.rotation);
    } //END ShootQuick()


    public void BecomeDouble()
    {
        spriteRenderer.sprite = doublePlayer;
        doubleShip = true;
        
    }

    private IEnumerator ShootCooldown()
    {
        canShootDefault = false;
        yield return new WaitForSeconds(shootDefaultCooldown);
        canShootDefault = true;
    } //END ShootCooldow()
}
