using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    enum State
    {
        UNHOOKED,
        LAUNCHING,
        HOOKED,
    }

    public float ropeLength = 3;
    public float pullForce = 3;
    public float launchDistance = 5;
    public float launchSpeed = 2;
    public float hookRadius = 0.5f;

    public AccelerationPhysic accelerationPhysic;
    public Camera cam;
    public GameObject enemyList;

    LineRenderer ropeLineRenderer;
    LineRenderer aimDirLineRenderer;

    GameObject hookedTo;
    State state = State.UNHOOKED;
    Vector2 hookPos = new Vector2();
    Vector2 launchingDir = new Vector2();

    public void Unhook()
    {
        if (state != State.HOOKED)
        {
            return;
        }
        state = State.UNHOOKED;
        hookedTo.GetComponent<EnemyAI>().state = EnemyAI.State.IDLE;
        hookedTo = null;
    }

    void Start()
    {
        ropeLineRenderer = transform.Find("rope").GetComponent<LineRenderer>();
        aimDirLineRenderer = transform.Find("hookAimDir").GetComponent<LineRenderer>();
    }

    void Launch(Vector2 dir)
    {
        state = State.LAUNCHING;
        hookPos = transform.position;
        launchingDir = GetAimDir();
    }

    void Update()
    {
        HookPhysic();
        DrawLines();
        LaunchUpdate();

        if (Input.GetMouseButtonDown(0))
        {
            Launch(GetAimDir());
        }
    }

    void DrawLines()
    {
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 aimDir = GetAimDir();
        aimDirLineRenderer.SetPosition(0, playerPos);
        aimDirLineRenderer.SetPosition(1, playerPos + aimDir * 2);

        if (state == State.UNHOOKED)
        {
            ropeLineRenderer.SetPosition(0, Vector2.zero);
            ropeLineRenderer.SetPosition(1, Vector2.zero);
        }
        else if (state == State.LAUNCHING)
        {
            ropeLineRenderer.SetPosition(0, transform.position);
            ropeLineRenderer.SetPosition(1, hookPos);
        }
        else if (state == State.HOOKED)
        {
            ropeLineRenderer.SetPosition(0, transform.position);
            ropeLineRenderer.SetPosition(1, hookedTo.transform.position);
        }
    } 

    void HookPhysic()
    {
        if (state != State.HOOKED)
        {
            return;
        }

        float boatDistance = Vector2.Distance(hookedTo.transform.position, transform.position);
        if (boatDistance > ropeLength) {
            AccelerationPhysic accelerationPhysic_hookedTo = hookedTo.GetComponent<AccelerationPhysic>();
            Vector2 ropeDir = (hookedTo.transform.position - transform.position).normalized;
            accelerationPhysic.AddToMoveVec(GetPullVec(ropeDir, boatDistance) * accelerationPhysic.affectedByHook);
            accelerationPhysic_hookedTo.AddToMoveVec(- GetPullVec(ropeDir, boatDistance) * accelerationPhysic_hookedTo.affectedByHook);
        }
    }

    void LaunchUpdate()
    {
        if (state != State.LAUNCHING)
        {
            hookPos = transform.position;
            launchingDir = Vector2.zero;
            return;
        }

        hookPos += launchingDir * launchSpeed * Time.deltaTime;
        if (Vector2.Distance(hookPos, transform.position) > launchDistance)
        {
            state = State.UNHOOKED;
            return;
        }

        for (int i = 0; i < enemyList.transform.childCount; i++)
        {
            GameObject enemy = enemyList.transform.GetChild(i).gameObject;
            Vector2 enemyPos = enemy.transform.position;

            if (Vector2.Distance(hookPos, enemyPos) < hookRadius)
            {
                if (hookedTo != null)
                {
                    hookedTo.GetComponent<EnemyAI>().state = EnemyAI.State.IDLE;
                }

                hookedTo = enemy;
                enemy.GetComponent<EnemyAI>().state = EnemyAI.State.ESCAPING;

                state = State.HOOKED;
                return;
            }
        }
    }

    Vector2 GetPullVec(Vector2 ropeDir, float boatDistance)
    {
        return ropeDir * pullForce * Time.deltaTime * (boatDistance - ropeLength);
    }

    Vector2 GetAimDir()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 aimDir = (mousePos - playerPos).normalized;
        return aimDir;
    }
}
