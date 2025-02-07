using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    private float speed;
    [SerializeField] float baseSpeed;
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    private bool moveLeft;
    private int count = 0;

    private void Start()
    {
        speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        MoveLeftRight();
    }

    private void MoveLeftRight()
    {
        if(moveLeft)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }


        if(transform.position.x <= xMin)
        {
            moveLeft = false;
             if(count == 0)
            {
                count++;
                StartCoroutine(PauseMove());
            }
        }
        else if (transform.position.x >= xMax)
        {
            moveLeft = true;
            if (count == 0)
            {
                count++;
                StartCoroutine(PauseMove());
            }
        }
    }

    IEnumerator PauseMove()
    {
        
        speed = 0;
        yield return new WaitForSeconds(0.5f);
        speed = baseSpeed;
        count = 0;
    }
}
