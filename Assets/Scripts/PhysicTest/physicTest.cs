using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physicTest : MonoBehaviour
{
    public float moveAcceleration = 4;
    public float reverseAcceleration = 4;
    public float moveSpeedCap = 5;

    public Rigidbody2D rb;

    Vector2 moveVec = new Vector2();

    void Start()
    {
        
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 moveDir = new Vector2(horizontalInput, verticalInput).normalized;

        calcMoveVec(moveDir);

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
}
