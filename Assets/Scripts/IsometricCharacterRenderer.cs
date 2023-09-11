using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCharacterRenderer : MonoBehaviour
{
    public static readonly string[] directions = {
        "North",
        "NortWest",
        "West",
        "SouthWest",
        "South",
        "SouthEast",
        "East",
        "NorthEast",
    };

    public Animator animator;

    public void SetDirection(Vector2 direction)
    {
        animator.Play(directions[DirectionToIndex(direction, 8)]);
    }

    public static int DirectionToIndex(Vector2 dir, int sliceCount)
    {
        Vector2 normDir = dir.normalized;

        float step  = 360f / sliceCount;

        float halfstep = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, normDir);

        angle += halfstep;

        if (angle<0)
        {
            angle += 360;
        }

        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }
}
