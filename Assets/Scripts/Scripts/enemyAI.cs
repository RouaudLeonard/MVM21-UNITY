using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float attackCooldownSec = 1;
    public float attackDamage = 25;

    public AccelerationPhysic accelerationPhysic;

    public enum State
    {
        IDLE,
        CHASING,
        ESCAPING,
    }

    GameObject attackZone;
    GameObject visionZone;
    GameObject chasingZone;
    GameObject player;

    public State state = State.IDLE;
    public bool enabled = true;
    float attackCooldownCount = 0;

    void Start()
    {
        attackZone = transform.Find("attackZone").gameObject;
        visionZone = transform.Find("visionZone").gameObject;
        chasingZone = transform.Find("chasingZone").gameObject;

        player = transform.parent.gameObject.GetComponent<EnemyList>().player;
    }

    void Update()
    {
        attackCooldownCount -= Time.deltaTime;

        if (state == State.IDLE)
        {
            IdleHandle();
        }
        else if (state == State.CHASING)
        {
            ChasingHandle();
        }
        else if (state == State.ESCAPING)
        {
            EscapingHandle();
        }
    }

    void IdleHandle()
    {
        if (DistanceFromPlayer() < visionZone.GetComponent<CircleRenderer>().range)
        {
            state = State.CHASING;
        }
    }

    void ChasingHandle()
    {
        float distanceFromPlayer = DistanceFromPlayer();

        if (distanceFromPlayer < attackZone.GetComponent<CircleRenderer>().range)
        {
            if (attackCooldownCount < 0)
            {
                attackCooldownCount = attackCooldownSec;
                Attack();
            }
        }

        if (distanceFromPlayer >= chasingZone.GetComponent<CircleRenderer>().range)
        {
            state = State.IDLE;
        }

        if (distanceFromPlayer >= attackZone.GetComponent<CircleRenderer>().range)
        {
            MoveToPlayer();
        }
    }

    void EscapingHandle()
    {
        MoveAwayFromPlayer();
    }

    float DistanceFromPlayer()
    {
        return new Vector2(player.transform.position.x - transform.position.x,
                            player.transform.position.y - transform.position.y).magnitude;
    }

    void Attack()
    {
        player.GetComponent<PlayerHealth>().CurrentHealth -= attackDamage;
    }

    void MoveToPlayer()
    {
        if (!enabled)
        {
            return;
        }
        
        Vector2 dir = new Vector2(player.transform.position.x - transform.position.x,
                                    player.transform.position.y - transform.position.y).normalized;
        accelerationPhysic.Move(dir);
    }

    void MoveAwayFromPlayer()
    {
        if (!enabled)
        {
            return;
        }
        
        Vector2 dir = new Vector2(player.transform.position.x - transform.position.x,
                                    player.transform.position.y - transform.position.y).normalized;
        accelerationPhysic.Move(- dir);
    }
}
