using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnergy : MonoBehaviour
{
    public float maxEnergy = 100;
    public float energyDepleteSpeed = 10;

    public Bar energyBar;
    public EnemyAI enemyAI;

    float currentEnergy = 0;
    public float CurrentEnergy
    {
        get { return currentEnergy; }
        set {
            if (currentEnergy == 0)
            {
                return;
            }

            currentEnergy = value;

            if (currentEnergy > maxEnergy)
            {
                currentEnergy = maxEnergy;
            }
            if (currentEnergy <= 0)
            {
                currentEnergy = 0;
                EnergyDepleted();
            }

            energyBar.filled = currentEnergy / maxEnergy;
        }
    }

    void Start()
    {
        currentEnergy = maxEnergy;
    }

    void Update()
    {
        if (enemyAI.state == EnemyAI.State.ESCAPING) {
            CurrentEnergy -= energyDepleteSpeed * Time.deltaTime;
        }
    }

    void EnergyDepleted()
    {
        print("enemy died");
    }
}
