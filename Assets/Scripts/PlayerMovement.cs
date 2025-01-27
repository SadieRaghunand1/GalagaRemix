using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls playerControls;
    float horizontal;
    float vertical;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    [SerializeField] GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        InitValues();
        //Testing
        //Instantiate(projectile);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    void InitValues()
    {
        //Attach player controls for movement
        playerControls = new PlayerControls();
        playerControls.Keyboard.MoveSide.performed += ctx => horizontal = ctx.ReadValue<float>();
        playerControls.Keyboard.MoveSide.canceled += _ => horizontal = 0;

        playerControls.Keyboard.MoveUpDown.performed += ctx => vertical = ctx.ReadValue<float>();
        playerControls.Keyboard.MoveUpDown.canceled += _ => vertical = 0;

        //Append Shoot() to shoot action
        playerControls.Keyboard.Shoot.started += _ => Shoot();
    }

    private void Move()
    {
        Debug.Log("Horizontal, Vertical: " + horizontal + ", " + vertical);
        rb.velocity = new Vector2(horizontal * speed, vertical);

    } //END Walk()

    private void Shoot()
    {
        Debug.Log("Shoot");
        Instantiate(projectile);
        
    } //END Shoot
}
