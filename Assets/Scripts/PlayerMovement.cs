using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls playerControls;
    float horizontal;
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



    private void FixedUpdate()
    {
        Move();
    }

    void InitValues()
    {
        //Attach player controls for movement
        playerControls = new PlayerControls();
        playerControls.Player.Movement.performed += ctx => horizontal = ctx.ReadValue<float>();
        playerControls.Player.Movement.canceled += _ => horizontal = 0;


        //Append Shoot() to shoot action
        playerControls.Player.Shoot.started += _ => Shoot();
    }

    private void Move()
    {
        Debug.Log("Horizontal " + horizontal);
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

    } //END Walk()

    private void Shoot()
    {
        Debug.Log("Shoot");
        Instantiate(projectile);
        
    } //END Shoot
}
