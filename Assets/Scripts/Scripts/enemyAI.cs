using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public AccelerationPhysic accelerationPhysic;

    enum State
    {
        IDLE,
        CHASING,
        ESCAPING,
    }

    GameObject attackZone;
    GameObject visionZone;
    GameObject chasingZone;
    GameObject player;

    State state = State.IDLE;
    bool attacked = false;

    void Start()
    {
        attackZone = transform.Find("attackZone").gameObject;
        visionZone = transform.Find("visionZone").gameObject;
        chasingZone = transform.Find("chasingZone").gameObject;

        player = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()[1];
    }

    void Update()
    {
        if (state == State.IDLE)
        {
            IdleHandle();
        }
        else if (state == State.CHASING)
        {
            ChasingHandle();
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
            if (!attacked)
            {
                attacked = true;
                Attack();
            }
        }
        else if (distanceFromPlayer >= attackZone.GetComponent<CircleRenderer>().range)
        {
            attacked = false;
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

    float DistanceFromPlayer()
    {
        return new Vector2(player.transform.position.x - transform.position.x,
                            player.transform.position.y - transform.position.y).magnitude;
    }

    void Attack()
    {
        print("Attack");
    }

    void MoveToPlayer()
    {
        Vector2 dir = new Vector2(player.transform.position.x - transform.position.x,
                                    player.transform.position.y - transform.position.y).normalized;
        accelerationPhysic.Move(dir);
    }
}
