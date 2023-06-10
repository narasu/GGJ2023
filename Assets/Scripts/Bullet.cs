using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 5;
    private Rigidbody rb;
    public float Damage = 50;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, life);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyController>())
        {
            print("we hebben een houthakker geraakt");
            Destroy(gameObject);
            other.gameObject.GetComponent<EnemyHealth>().EnemyTakeDamge(Damage);
        }
    }
}
