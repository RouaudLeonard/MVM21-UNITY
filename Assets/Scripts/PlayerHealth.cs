using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update

    float currentHealth;
    float maxHealth = 100;
    LayerMask Enemy;
    float enemyDamage = 5f;
   private Transform playerPos;
    Transform enemyPos;
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyPos == playerPos)
        {
            return;

            currentHealth = currentHealth - enemyDamage;

            Debug.Log("You are now at " + currentHealth + "HP");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
//if gets hit by enemy or enemy fire, deal damage.
    }
}
