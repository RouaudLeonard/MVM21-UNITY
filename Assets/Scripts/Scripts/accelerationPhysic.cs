using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationPhysic : MonoBehaviour
{
    public float moveAcceleration = 4;
    public float reverseAcceleration = 4;
    public float moveSpeedCap = 5;
    public float affectedByHook = 1;

    public bool isPlayer = false;

    public Rigidbody2D rb;

    Vector2 moveVec = new Vector2();

    public void AddToMoveVec(Vector2 force)
    {
        moveVec += force;
    }

    void Update()
    {
        if (isPlayer)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            Vector2 moveDir = new Vector2(horizontalInput, verticalInput).normalized;

            CalcMoveVec(moveDir);

            rb.velocity = moveVec;

            return;
        }

        CalcMoveVec(Vector2.zero);
        rb.velocity = moveVec;
    }

    void CalcMoveVec(Vector2 moveDir)
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
