using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artificial_health_take_down_script : MonoBehaviour
{
    public PlayerHealth health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            health.currentHealth -= 100;
        }
    }
}
