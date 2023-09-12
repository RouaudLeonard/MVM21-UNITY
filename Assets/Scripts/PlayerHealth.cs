using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;

    public Bar healthBar;

    float currentHealth = 0;
    public float CurrentHealth
    {
        get { return currentHealth; }
        set {
            if (currentHealth == 0)
            {
                return;
            }

            currentHealth = value;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            if (currentHealth < 0)
            {
                currentHealth = 0;
                PlayerDied();
            }

            healthBar.filled = currentHealth / maxHealth;
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    void PlayerDied()
    {
        
    }
}
