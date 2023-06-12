using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Buildings : MonoBehaviour
{
    public static Buildings instance;
    public float projectileSpeed = 100;
    private Animator anim;
    [HideInInspector] public Rigidbody rb;
    public GameObject projectilesPrefab;
    public Transform projectileSpawnPoint;
    private FOV fov;
    private float _timer;

    RaycastHit hit;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        fov = this.gameObject.GetComponent<FOV>();
        PauseMenu.Instance.towers.Add(this.gameObject);
    }

    public void shootSeeds(Transform enemyTransform)
    {
        _timer += Time.deltaTime;
        //when enemy is found, the tower locks on to the enemy.
        this.gameObject.transform.LookAt(enemyTransform.position + new Vector3(0, 0, 3));
        if (_timer > 1f)
        {
            _timer = 0;
            var Bullet = Instantiate(projectilesPrefab, projectileSpawnPoint.position, projectilesPrefab.transform.rotation);
            //Bullet.GetComponent<Rigidbody>().velocity = projectileSpawnPoint.forward * projectileSpeed;
            Bullet.transform.LookAt(hit.transform);
            StartCoroutine(Bullet.GetComponent<Bullet>().SendHoming(enemyTransform, projectileSpeed));

        }
    }

    public void CheckForEnemies()
    {
        print("we're checking" + gameObject.transform.position);
        //check of er enemies in de lijst zitten om een error te voorkomen
        if (fov.visibleTarget.Count != 0)
        {
            print(" er is een enemy");
            //zet de animation uit zodat je de transform van de player kan aanpassen.
            anim.SetBool("IsPatrolling", false);
            anim.enabled = false;
            //kijk naar de positie van de target. er zit maar een target in de lijst dus het is altijd de eerste target in de lijst.
            this.gameObject.transform.LookAt(fov.visibleTarget[0].transform.position);
            //start het shoot script en geef het de transform van de enemy mee zodat de bullets naar de enemy kunnen gaan.
            shootSeeds(fov.visibleTarget[0].transform);
        }
        else
        {
            anim.enabled = true;
            anim.SetBool("IsPatrolling", true);
        }

    }
}

