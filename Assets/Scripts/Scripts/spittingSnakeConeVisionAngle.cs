using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpittingSnakeConeVisionAngle : MonoBehaviour
{
    public float angleDeg = 40;
    public float nearRange = 2;
    public LineRenderer lr;

    Vector2 dir = new Vector2();

    GameObject player;

    bool show = false;
    RaycastHit2D latestRaycastResult;

    void Start()
    {
        Transform dirTransform = transform.parent.transform.Find("coneVisionDirection");
        dir = new Vector2(dirTransform.position.x - transform.position.x,
                            dirTransform.position.y - transform.position.y).normalized;

        player = transform.parent.transform.parent.gameObject.GetComponent<EnemyList>().player;
    }

    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            show = !show;
        }

        UpdateLatestRaycastResult();

        if (!show)
        {
            for (int i = 0; i < 6; i++)
            {
                lr.SetPosition(i, Vector2.zero);
            }
            return;
        }

        Vector2 dir_0 = dir;
        Vector2 dir_1 = dir;
        Vector2 dir_2 = new Vector2();

        dir_0 = Quaternion.Euler(0, 0, -angleDeg/2) * dir_0;
        dir_1 = Quaternion.Euler(0, 0,  angleDeg/2) * dir_1;

        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        lr.SetPosition(0, pos);
        lr.SetPosition(1, pos + dir_0 * 10);
        lr.SetPosition(2, pos);
        lr.SetPosition(3, pos + dir_1 * 10);

        Vector2 playerDir = new Vector2(player.transform.position.x - transform.position.x,
                                    player.transform.position.y - transform.position.y).normalized;
        lr.SetPosition(4, pos + playerDir * nearRange);
        lr.SetPosition(5, latestRaycastResult.point);
    }

    bool CanSeePlayer()
    {
        return NoObstacle() && PlayerIsInsideAngle();
    }

    bool NoObstacle()
    {
        return latestRaycastResult.transform == player.transform;
    }

    bool PlayerIsInsideAngle()
    {
        Vector2 playerDir = new Vector2(player.transform.position.x - transform.position.x,
                                    player.transform.position.y - transform.position.y).normalized;
        return Mathf.Abs(Vector2.SignedAngle(dir, playerDir)) < angleDeg/2;
    }

    void UpdateLatestRaycastResult()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 playerDir = new Vector2(player.transform.position.x - transform.position.x,
                                    player.transform.position.y - transform.position.y).normalized;
        latestRaycastResult = Physics2D.Raycast(pos + playerDir * nearRange, playerDir);
    }
}
