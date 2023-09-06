using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPhysicTest : MonoBehaviour
{
    public float moveAcceleration = 4;
    public float reverseAcceleration = 4;
    public float moveSpeedCap = 5;

    public Rigidbody2D rb;

    Vector2 moveVec = new Vector2();

    public void AddToMoveVec(Vector2 force)
    {
        moveVec += force;
    }

    void Update()
    {
        calcMoveVec(new Vector2(0, 0));

        rb.velocity = moveVec;
    }

    void calcMoveVec(Vector2 moveDir)
    {
        if (moveDir == Vector2.zero) 
        {
            if (Mathf.Abs(moveVec.magnitude) < 0.1)
            {
                moveVec = Vector2.zero;
                return;
            }

            moveVec -= moveVec.normalized * reverseAcceleration * Time.deltaTime;
            return;
        }

        moveVec += moveDir * moveAcceleration * Time.deltaTime;
        if (moveVec.magnitude > moveSpeedCap)
        {
            moveVec = moveVec.normalized * moveSpeedCap;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        moveVec = Vector2.zero;
    }
}
