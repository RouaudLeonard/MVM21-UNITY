using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;

    public Bar healthBar;

    // Init to 1 because if init to 0 CurrentHealth property wont be able to modify currentHealth field
    float currentHealth = 1;
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
        CurrentHealth = maxHealth;
    }

    void PlayerDied()
    {
        
    }
}
