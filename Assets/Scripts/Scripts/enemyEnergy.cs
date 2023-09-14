using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnergy : MonoBehaviour
{
    public float maxEnergy = 100;
    public float energyDepleteSpeed = 10;
    public float capturedHealthAdd = 25;

    public Bar energyBar;
    public EnemyAI enemyAI;

    GameObject player;

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
        player = transform.parent.gameObject.GetComponent<EnemyList>().player;
    }

    void Update()
    {
        if (enemyAI.state == EnemyAI.State.ESCAPING) {
            CurrentEnergy -= energyDepleteSpeed * Time.deltaTime;
        }
    }

    void EnergyDepleted()
    {
        player.GetComponent<PlayerHealth>().CurrentHealth += capturedHealthAdd;
        player.GetComponent<Hook>().Unhook();
        Destroy(gameObject);
    }
}
