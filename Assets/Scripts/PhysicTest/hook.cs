using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook : MonoBehaviour
{
    public float ropeLength = 10;
    public float pullForce = 4;

    public GameObject hookedTo;
    public LineRenderer lineRenderer;
    public physicTest physic;

    void Update()
    {
        hookPhysic();
        drawLine();
    }

    void drawLine()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, hookedTo.transform.position);
    } 

    void hookPhysic()
    {
        float boatDistance = Vector2.Distance(hookedTo.transform.position, transform.position);
        if (boatDistance > ropeLength) {
            Vector2 ropeDir = (hookedTo.transform.position - transform.position).normalized;
            physic.AddToMoveVec(getPullVec(ropeDir, boatDistance));
            hookedTo.GetComponent<enemyPhysicTest>().AddToMoveVec(- getPullVec(ropeDir, boatDistance));
        }
    }

    Vector2 getPullVec(Vector2 ropeDir, float boatDistance)
    {
        return ropeDir * pullForce * Time.deltaTime * (boatDistance - ropeLength);
    }
}
