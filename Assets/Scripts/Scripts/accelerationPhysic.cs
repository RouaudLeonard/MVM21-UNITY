using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationPhysic : MonoBehaviour
{
    public float moveAcceleration = 4;
    public float reverseAcceleration = 4;
    public float moveSpeedCap = 5;

    public float turnAccelerationDeg = 30;
    public float turnReverseAccelerationDeg = 30;
    public float turnSpeedCapDegPerSec = 180;

    public float affectedByHook = 1;

    public bool isPlayer = false;

    public Rigidbody2D rb;

    Vector2 moveVec = new Vector2();

    LineRenderer lookDirLineRenderer;
    Vector2 lookDir = Vector2.up;
    float turnSpeedDeg = 0;

    public void AddToMoveVec(Vector2 force)
    {
        moveVec += force;
    }

    void Start()
    {
        lookDirLineRenderer = transform.Find("lookDir").GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (isPlayer)
        {
            DrawLookDir();

            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            CalcTurnSpeed(horizontalInput);

            lookDir = Quaternion.Euler(0, 0, turnSpeedDeg * Time.deltaTime) * lookDir;
            Vector2 moveDir = lookDir * verticalInput;

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

    void DrawLookDir()
    {
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
        lookDirLineRenderer.SetPosition(0, transform.position);
        lookDirLineRenderer.SetPosition(1, playerPos + lookDir * 1);
    }

    void CalcTurnSpeed(float horizontalInput)
    {
        if (horizontalInput != 0)
        {
            if (horizontalInput > 0)
            {
                turnSpeedDeg -= turnAccelerationDeg * Time.deltaTime;
            }
            if (horizontalInput < 0)
            {
                turnSpeedDeg += turnAccelerationDeg * Time.deltaTime;
            }

            turnSpeedDeg = Mathf.Clamp(turnSpeedDeg, -turnSpeedCapDegPerSec, turnSpeedCapDegPerSec);
            return;
        }

        if (Mathf.Abs(turnSpeedDeg) < 0.1)
        {
            turnSpeedDeg = 0;
        }
        else if (turnSpeedDeg > 0)
        {
            turnSpeedDeg -= turnReverseAccelerationDeg * Time.deltaTime;
        }
        else if (turnSpeedDeg < 0)
        {
            turnSpeedDeg += turnReverseAccelerationDeg * Time.deltaTime;
        }
    }
}
