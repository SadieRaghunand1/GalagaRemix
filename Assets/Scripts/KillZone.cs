using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        KillObject(collision);
    }

    void KillObject(Collider2D _collision)
    {
        if(_collision.gameObject.layer == 7)
        {
            Destroy(_collision.gameObject);
        }
    }
}
