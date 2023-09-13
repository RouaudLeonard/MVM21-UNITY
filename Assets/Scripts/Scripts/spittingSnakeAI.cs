using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpittingSnakeAI : MonoBehaviour
{
    public GameObject visionConeDirection;
    public GameObject turretPoint;

    GameObject coneVisionRange;
    GameObject coneVisionAngle;

    void Start()
    {
        coneVisionRange = transform.Find("coneVisionRange").gameObject;
        coneVisionAngle = transform.Find("coneVisionAngle").gameObject;
    }

    void Update()
    {
        
    }
}
