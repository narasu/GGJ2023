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
            other.gameObject.GetComponent<EnemyHealth>().EnemyTakeDamge(Damage);
            Destroy(this.gameObject);
        }
    }
    public IEnumerator SendHoming(Transform enemyTransform, float projectileSpeed)
    {
        while (true)
        {
            if (enemyTransform == null)
            {
                Destroy(gameObject);
                break;
            }
            transform.position += (enemyTransform.position - transform.position).normalized * projectileSpeed * Time.deltaTime;
            transform.LookAt(enemyTransform.transform);
            yield return null;
        }
    }
}
