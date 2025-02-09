using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S3Boss : BossBehavior
{


    private float yOffset = 0;
    private float min = 1;
    private float max = 4;

    


    private void Start()
    {
        StartCoroutine(TimeShoot());
    }

    protected override void Shoot()
    {
        Instantiate(projectile, new Vector2(transform.position.x, transform.position.y + yOffset), projectile.transform.localRotation);
        StartCoroutine(TimeShoot());
    }

    private IEnumerator TimeShoot()
    {
        yield return new WaitForSeconds(Random.Range(min, max));
        Shoot();

    }
}
