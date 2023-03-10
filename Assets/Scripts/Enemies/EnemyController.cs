using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;
    NavMeshAgent Enemy;
    [SerializeField] private float Speed = 10;
    public EnemyHealth EnemyHealth;

    private void Awake()
    {
        Instance = this;
        Enemy = GetComponent<NavMeshAgent>();
        Enemy.speed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        Enemy.SetDestination(PlayerController.Instance.Player.position);
    }
}
