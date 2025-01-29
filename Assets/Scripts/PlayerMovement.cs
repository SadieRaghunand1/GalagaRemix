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

    [SerializeField] GameObject projectile;
    [SerializeField] private float yOffset;

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
    /// Shoot projectile on button press
    /// </summary>
    private void Shoot()
    {
        Debug.Log("Shoot");
        Instantiate(projectile, new Vector2(transform.position.x, transform.position.y + yOffset), projectile.transform.rotation);
        
    } //END Shoot
}
