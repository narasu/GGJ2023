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

        StartCoroutine(DamagePerSecond());
    }


    public void EnemyTakeDamge(int amount)
    {
        currentEnemyHealth -= amount;

        if (currentEnemyHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator DamagePerSecond()
    {
        while (currentEnemyHealth > 0)
        {
            EnemyTakeDamge(10);
            yield return new WaitForSeconds(1.0f);
        }
        
    }

    void Die()
    {
        //we're dead
        //GetComponent<Lootbag>().InstantiateLoot(transform.position);
        Destroy(gameObject);
    }
}
