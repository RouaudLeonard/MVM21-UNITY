using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float ropeLength = 3;
    public float pullForce = 3;

    public LineRenderer lineRenderer;
    public AccelerationPhysic accelerationPhysic;

    public GameObject hookedTo;

    void Update()
    {
        if (hookedTo == null)
        {
            return;
        }

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
            accelerationPhysic.AddToMoveVec(getPullVec(ropeDir, boatDistance));
            hookedTo.GetComponent<AccelerationPhysic>().AddToMoveVec(- getPullVec(ropeDir, boatDistance));
        }
    }

    Vector2 getPullVec(Vector2 ropeDir, float boatDistance)
    {
        return ropeDir * pullForce * Time.deltaTime * (boatDistance - ropeLength);
    }
}
