using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int EnemyDamage;
    private void OnCollisionStay(Collision other)
    {
        if (other.collider.GetComponent<PlayerHealth>() != null)
        {
            //if the enemy comes into contact with the player it'll do damage.
            var healthComponent = other.collider.GetComponent<PlayerHealth>();
            if (healthComponent != null)
            {
                print("we're attacking (enemy)");
                healthComponent.takeDamage(EnemyDamage);
            }

        }
    }
}
