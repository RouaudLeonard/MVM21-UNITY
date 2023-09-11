using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public float w = 1;
    public float h = 0.15f;

    LineRenderer bkgLr;
    LineRenderer filledLr;

    public float filled = 1;

    void Start()
    {
        bkgLr = transform.Find("bkg").GetComponent<LineRenderer>();
        filledLr = transform.Find("filled").GetComponent<LineRenderer>();
    }

    void Update()
    {
        bkgLr.startWidth = h;
        bkgLr.endWidth = h;
        filledLr.startWidth = h;
        filledLr.endWidth = h;

        bkgLr.SetPosition(0, new Vector2(transform.position.x - w, transform.position.y));
        bkgLr.SetPosition(1, new Vector2(transform.position.x + w, transform.position.y));
        filledLr.SetPosition(0, new Vector2(transform.position.x - w, transform.position.y));
        filledLr.SetPosition(1, new Vector2(transform.position.x - w + filled * w * 2, 
                                                transform.position.y));
    }
}
