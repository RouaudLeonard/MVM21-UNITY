using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotcutBarrier : MonoBehaviour
{
    public GameObject enemy;

    void Update()
    {
        if (enemy.GetComponent<EnemyEnergy>().CurrentEnergy == 0)
        {
            Destroy(gameObject);
        }
    }
}
