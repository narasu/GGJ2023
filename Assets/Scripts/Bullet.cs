using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;
    public GameObject Enemy;
    public float force;
    private Rigidbody rb;
    private RaycastHit _hit;
    float projectileSpeed = 100;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, life);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<EnemyController>())
        {
            //print("we hebben een houthakker geraakt");
            //Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyController>())
        {
            print("we hebben een houthakker geraakt");
            //Destroy(gameObject);
        }
    }
}
