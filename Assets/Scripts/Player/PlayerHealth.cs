using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth;
    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        //make sure health is set to max health
        currentHealth = MaxHealth;
    }

    //this function allows other object to damage the player
    public void takeDamage(int amount)
    {
        print(currentHealth);
        currentHealth -= amount;
        //once health drops below 0, you're dead
        if (currentHealth <= 0)
        {
            //we're dead
            Destroy(gameObject);
        }
    }
}
