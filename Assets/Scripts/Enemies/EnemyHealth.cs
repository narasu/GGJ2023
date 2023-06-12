using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public static EnemyHealth Instance;
    public float MaxEnemyHealth;
    public float currentEnemyHealth;

    void Awake()
    {
        currentEnemyHealth = MaxEnemyHealth; //make sure the current health is set to the max
        Instance = this;

    }


    public void EnemyTakeDamge(float amount)
    {
        currentEnemyHealth -= amount;

        if (currentEnemyHealth <= 0)
        {
            //geef de player de punten die hij heeft verdient met deze kill
            PlayerController.Instance.currency += GetComponent<EnemyController>().enemyWorth;
            FOV.instance.addedTolist = false;
            Die();
        }
    }

    void Die()
    {
        //we're dead
        Destroy(gameObject);
    }
}
