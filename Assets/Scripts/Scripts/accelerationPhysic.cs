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

    public bool hasShell = false;

    public bool isPlayer = false;
    public bool useDirectionBasedMovement = false;

    public Rigidbody2D rb;
    public IsometricCharacterRenderer icr;

    Vector2 latestMoveVec = Vector2.right;
    Vector2 moveVec = new Vector2();
    Vector2 moveDir = new Vector2();

    LineRenderer lookDirLineRenderer;
    Vector2 lookDir = Vector2.up;
    float turnSpeedDeg = 0;

    public bool HookedToShell(Vector2 hookDir)
    {
        if (!hasShell)
        {
            return false;
        }

        if (Vector2.Dot(latestMoveVec.normalized, hookDir.normalized) > 0)
        {
            print(Vector2.Dot(latestMoveVec.normalized, hookDir.normalized));
            return true;
        }

        return false;
    }

    public void AddToMoveVec(Vector2 force)
    {
        moveVec += force;
    }

    public void Move(Vector2 inMoveDir)
    {
        moveDir = inMoveDir;
    }

    void Start()
    {
        lookDirLineRenderer = transform.Find("lookDir").GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (moveVec != Vector2.zero)
        {
            latestMoveVec = moveVec;
        }

        if (isPlayer)
        {
            SetPlayerAnimationDir();

            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            CalcTurnSpeed(horizontalInput);

            lookDir = Quaternion.Euler(0, 0, turnSpeedDeg * Time.deltaTime) * lookDir;

            moveDir = new Vector2(horizontalInput, verticalInput);
            if (useDirectionBasedMovement)
            {
                moveDir = lookDir * verticalInput;
            }

            CalcMoveVec(moveDir);
            moveDir = Vector2.zero;

            rb.velocity = moveVec;

            return;
        }

        DrawLookDir();

        CalcMoveVec(moveDir);
        moveDir = Vector2.zero;
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

        if (!isPlayer)
        {
            return;
        }

        GetComponent<Hook>().Unhook();
    }

    void DrawLookDir()
    {
        if (!isPlayer)
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            lookDirLineRenderer.SetPosition(0, transform.position);
            lookDirLineRenderer.SetPosition(1, pos + latestMoveVec.normalized * 1);
            return;
        }

        if (!useDirectionBasedMovement)
        {
            lookDirLineRenderer.SetPosition(0, Vector2.zero);
            lookDirLineRenderer.SetPosition(1, Vector2.zero);
            return;
        }

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

    void SetPlayerAnimationDir()
    {
        icr.SetDirection(latestMoveVec);
    }
}
