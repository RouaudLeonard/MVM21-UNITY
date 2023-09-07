using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public float clampW = 5;
    public float clampH = 5;
    public GameObject player;

    void Update()
    {
        Vector3 nextCamPos = new Vector3();
        nextCamPos.x = Mathf.Clamp(transform.position.x, player.transform.position.x - clampW / 2,
                                                                player.transform.position.x + clampW / 2);
        nextCamPos.y = Mathf.Clamp(transform.position.y, player.transform.position.y - clampH / 2,
                                                                player.transform.position.y + clampH / 2);
        nextCamPos.z = -10;

        transform.position = nextCamPos;
    }
}
