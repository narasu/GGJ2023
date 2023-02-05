using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public static EnemyHealth Instance;
    public float MaxEnemyHealth;
    public float currentEnemyHealth;

    void awake()
    {
        currentEnemyHealth = MaxEnemyHealth; //make sure the current health is set to the max
        Instance = this;
    }


    public void EnemyTakeDamge(int amount)
    {
        currentEnemyHealth -= amount;

        if (currentEnemyHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //we're dead
        //GetComponent<Lootbag>().InstantiateLoot(transform.position);
        Destroy(gameObject);
    }
}
