using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Buildings : MonoBehaviour
{
    public static Buildings instance;
    public float attackRange, projectileSpeed, damagePerHit;
    private Animator anim;
    [HideInInspector] public Rigidbody rb;
    public GameObject projectilesPrefab, enemy;
    public Transform projectileSpawnPoint;
    private float _timer;

    RaycastHit hit;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public void shootSeeds(Transform enemyTransform)
    {
        _timer += Time.deltaTime;
        //when enemy is found, the tower locks on to the enemy.
        this.gameObject.transform.LookAt(enemyTransform);
        if (_timer > 1f)
        {
            _timer = 0;
            var Bullet = Instantiate(projectilesPrefab, projectileSpawnPoint.position, projectilesPrefab.transform.rotation);
            //Bullet.GetComponent<Rigidbody>().velocity = projectileSpawnPoint.forward * projectileSpeed;
            Bullet.transform.LookAt(hit.transform);
            StartCoroutine(SendHoming(Bullet));

        }
    }

    public IEnumerator SendHoming(GameObject Bullet)
    {
            print("schiet te kogel");
            while (Vector3.Distance(enemy.transform.position, Bullet.transform.position) > 0.8f)
            {
                Bullet.transform.position += (enemy.transform.position - Bullet.transform.position).normalized * projectileSpeed * Time.deltaTime;
                Bullet.transform.LookAt(enemy.transform);
                yield return null;
            }
        Destroy(Bullet);
    }
    public void CheckForEnemies()
    {
        if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out hit, attackRange) && hit.transform.GetComponent<EnemyController>())
        {
            //print("enemy found");
            enemy = hit.transform.gameObject;
            shootSeeds(enemy.transform);
            anim.enabled = false;
        }
        else
        {
            print("we zien niks");
            anim.enabled = true;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.25f);
    }
}

