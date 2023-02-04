using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacledamage : MonoBehaviour
{
    [SerializeField] private int ObstacleDamage;
    private void OnCollisionEnter(Collision other) //if the bullet hits
    {
        if (other.collider.GetComponent<EnemyHealth>() != null) //and its true that the enemy is hit
        {
            var healthComponent = other.collider.GetComponent<EnemyHealth>(); //reference to enemy health script.
            healthComponent.EnemyTakeDamge(ObstacleDamage); //do damage
            Destroy(healthComponent.gameObject);//destroy the bullet
        }
    }
}
