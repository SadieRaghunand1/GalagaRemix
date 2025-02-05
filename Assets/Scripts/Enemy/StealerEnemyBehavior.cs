using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealerEnemyBehavior : EnemyBehavior
{

    [SerializeField] private Animator rayAnimator;
    [SerializeField] private GameObject playerCopy;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject capturedPlayer;
    private bool shipStolen = false;



    private void Update()
    {
        if(shipStolen)
        {
            capturedPlayer.GetComponent<Rigidbody2D>().MovePosition(transform.position);
        }
    }


    void StealShip()
    {
        //play ray animation or something

        capturedPlayer = Instantiate(playerCopy, player.transform.position, transform.rotation);
        capturedPlayer.transform.parent = transform;
    }
}
