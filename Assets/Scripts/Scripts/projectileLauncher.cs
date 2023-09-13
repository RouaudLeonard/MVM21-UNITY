using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public float projectileCooldownSec = 2;
    public GameObject projectilePrefab;

    GameObject projectileZone;
    GameObject player;
    GameObject enemyProjectileList;

    float projectileCooldownCount = 0;

    public bool enabled = false;

    void Start()
    {
        projectileZone = transform.Find("projectileZone").gameObject;
        player = transform.parent.gameObject.GetComponent<EnemyList>().player;
        enemyProjectileList = transform.parent.gameObject.GetComponent<EnemyList>().enemyProjectileList;
    }

    void Update()
    {
        float distanceFromPlayer = DistanceFromPlayer();
        projectileCooldownCount -= Time.deltaTime;

        if (projectileCooldownCount < 0 
        && distanceFromPlayer < projectileZone.GetComponent<CircleRenderer>().range)
        {
            projectileCooldownCount = projectileCooldownSec;
            LaunchProjectile();
        }
    }

    float DistanceFromPlayer()
    {
        return new Vector2(player.transform.position.x - transform.position.x,
                            player.transform.position.y - transform.position.y).magnitude;
    }

    void LaunchProjectile()
    {
        if (!enabled)
        {
            return;
        }
        
        Vector2 dir = new Vector2(player.transform.position.x - transform.position.x,
                                    player.transform.position.y - transform.position.y).normalized;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<Projectile>().dir = dir;
        projectile.transform.parent = enemyProjectileList.transform;
    }
}
