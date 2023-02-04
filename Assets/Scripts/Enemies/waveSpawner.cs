using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]

public class Wave
{
    //script for setting up individual waves
    public string waveName;
    public int NrOfEnemies;
    public GameObject[] TypeOfEnemies;
    public float spawnInterval;
}

public class waveSpawner : MonoBehaviour
{//script for spawning the enemies in waves
    public Wave[] wave;
    public Transform[] spawnpoint;
    private Wave CurrentWave;
    public Animator animator;
    public TextMeshProUGUI waveName;
    private int CurrentWaveNumber=0; //will help us know what wave we are in

    private bool canSpawn = true;
    private bool canAnimate = false;
    private float nextSpawnTime;
    void Update()
    {
        CurrentWave = wave[CurrentWaveNumber]; //if currentwavenr is 0 wave 1 is active and so on
        spawnWave(); //spawn a wave
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemies");
        if (totalEnemies.Length == 0)
        {
            if (CurrentWaveNumber + 1 != wave.Length)
            {
                if (canAnimate)
                {
                    waveName.text = wave[CurrentWaveNumber + 1].waveName;
                    animator.SetTrigger("WaveComplete");
                    canAnimate = false;
                }
            } else {
                Debug.Log("End Of the Game");
                animator.SetBool("Win", true);
            }
        }
    }

     void spawnNextWave()
     {
         CurrentWaveNumber++;
         canSpawn = true;
     }
    void spawnWave()
    {
        //only spawn an enemy if canspawn is true
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = CurrentWave.TypeOfEnemies[Random.Range(0, CurrentWave.TypeOfEnemies.Length)]; //select a random enemy from list
            Transform RandomPoint = spawnpoint[Random.Range(0, spawnpoint.Length)]; //select a random spawnpoint
            Instantiate(randomEnemy, RandomPoint.position, Quaternion.identity); //spawn the enemy
            CurrentWave.NrOfEnemies--; //decrease the amount of enemies it needs to spawn
            nextSpawnTime = Time.time + CurrentWave.spawnInterval; //countdown the spawntime interval

            if (CurrentWave.NrOfEnemies == 0)
            {
                //stop spawning if there's no more enemies in the list
                canSpawn = false;
                canAnimate = true;
            }
        }
    }
}
