using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 2;
    public float collisionRadius = 0.2f;
    public float attackDamage = 25;
    public float existForSec = 10;
    public Vector2 dir = new Vector2();

    GameObject player;
    float existedForSec = 0;

    void Start()
    {
        player = transform.parent.gameObject.GetComponent<EnemyProjectileList>().player;
    }

    void Update()
    {
        existedForSec += Time.deltaTime;
        if (existedForSec > existForSec)
        {
            Destroy(gameObject);
        }

        Vector3 dir3 = new Vector3(dir.x, dir.y, 0);
        transform.position += dir3 * Time.deltaTime * speed;

        if (DistanceFromPlayer() < collisionRadius)
        {
            player.GetComponent<PlayerHealth>().CurrentHealth -= attackDamage;
            Destroy(gameObject);
        }
    }

    float DistanceFromPlayer()
    {
        return new Vector2(player.transform.position.x - transform.position.x,
                            player.transform.position.y - transform.position.y).magnitude;
    }
}
