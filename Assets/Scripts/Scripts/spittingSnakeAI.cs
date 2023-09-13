using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpittingSnakeAI : MonoBehaviour
{
    enum State
    {
        STATIC,
        ATTACKING,
        HOOKED,
    }

    public GameObject turretPoint;
    public float timeToTurretPointSec = 2;
    public float timeUntilBackToOriginalPos = 10;
    public EnemyAI enemyAI;
    public ProjectileLauncher projectileLauncher;

    GameObject coneVisionRange;
    GameObject coneVisionAngle;
    GameObject projectileZone;
    GameObject player;

    Vector2 originalPos = new Vector2();
    Vector2 turretPos = new Vector2();

    float swapPlaceCountDownSec = 0;
    float timeUntilBackToOriginalPosCountDown = 0;

    State state = State.STATIC;
    State StateProperty
    {
        get { return state; }
        set 
        {
            state = value;

            if (state == State.STATIC)
            {
                projectileLauncher.enabled = false;
                enemyAI.enabled = false;

                swapPlaceCountDownSec = timeToTurretPointSec;

                // Handle animation here
            }
            else if (state == State.ATTACKING)
            {
                projectileLauncher.enabled = true;
                enemyAI.enabled = false;

                swapPlaceCountDownSec = timeToTurretPointSec;
                timeUntilBackToOriginalPosCountDown = timeUntilBackToOriginalPos;

                // Handle animation here
            }
            else if (state == State.HOOKED)
            {
                projectileLauncher.enabled = false;
                enemyAI.enabled = true;

                // Handle animation here
            }
        }
    }

    void Start()
    {
        originalPos = transform.position;
        turretPos = turretPoint.transform.position;
        enemyAI.enabled = false;
        coneVisionRange = transform.Find("coneVisionRange").gameObject;
        coneVisionAngle = transform.Find("coneVisionAngle").gameObject;
        projectileZone = transform.Find("projectileZone").gameObject;
        player = transform.parent.gameObject.GetComponent<EnemyList>().player;
    }

    void Update()
    {
        swapPlaceCountDownSec -= Time.deltaTime;
        timeUntilBackToOriginalPosCountDown -= Time.deltaTime;

        if (projectileZone.GetComponent<CircleRenderer>().range > DistanceFromPlayer())
        {
            timeUntilBackToOriginalPosCountDown = timeUntilBackToOriginalPos;
        }

        if (StateProperty != State.HOOKED && enemyAI.state == EnemyAI.State.ESCAPING)
        {
            StateProperty = State.HOOKED;
        }

        if (StateProperty == State.HOOKED && enemyAI.state != EnemyAI.State.ESCAPING)
        {
            StateProperty = State.STATIC;
        }

        if (StateProperty == State.STATIC)
        {
            if (CanSeePlayer())
            {
                StateProperty = State.ATTACKING;
            }

            if (swapPlaceCountDownSec >= 0)
            {
                transform.position = new Vector2(-999999, -999999);
            }
            else
            {
                transform.position = originalPos;
            }
        }

        else if (StateProperty == State.ATTACKING)
        {
            if (timeUntilBackToOriginalPosCountDown < 0)
            {
                StateProperty = State.STATIC;
            }

            if (swapPlaceCountDownSec >= 0)
            {
                transform.position = new Vector2(-999999, -999999);
            }
            else
            {
                transform.position = turretPos;
            }
        }
    }

    bool CanSeePlayer()
    {
        return coneVisionRange.GetComponent<CircleRenderer>().range > DistanceFromPlayer()
                && coneVisionAngle.GetComponent<SpittingSnakeConeVisionAngle>().CanSeePlayer();
    }

    float DistanceFromPlayer()
    {
        return new Vector2(player.transform.position.x - transform.position.x,
                            player.transform.position.y - transform.position.y).magnitude;
    }
}
