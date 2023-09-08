using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRenderer : MonoBehaviour
{
    const bool SHOW = true;
    
    public float range = 5;

    public LineRenderer lineRenderer;

    void Update()
    {
        if (!SHOW)
        {
            for (int i = 0; i < 20; i += 1)
            {
                lineRenderer.SetPosition(i, Vector2.zero);
            }

            return;
        }

        Vector2 pt0 = new Vector2(0, range);

        for (int i = 0; i < 20; i += 1)
        {
            Vector2 parentPos = new Vector2(transform.parent.transform.position.x, transform.parent.transform.position.y);
            Vector2 pt1 = Quaternion.Euler(0, 0, 360 / 18 * i) * pt0;
            lineRenderer.SetPosition(i, pt1 + parentPos);
        }
    }
}
