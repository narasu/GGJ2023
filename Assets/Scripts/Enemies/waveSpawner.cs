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
    public static waveSpawner Instance;
    public Wave[] wave;
    public Transform[] spawnpoint;
    private Wave CurrentWave;
    public Animator Animator;
    public TextMeshProUGUI waveName;
    public float EnemiesSpawned = 0;
    private int CurrentWaveNumber; //will help us know what wave we are in

    private bool canSpawn = false;
    private bool canAnimate = false;
    private float nextSpawnTime;

    private void Awake()
    {
        CurrentWaveNumber = 0;
        Instance = this;
    }
    void Update()
    {
        // print(CurrentWaveNumber);
        // print(wave[CurrentWaveNumber]);

        CurrentWave = wave[CurrentWaveNumber - 1]; //if currentwavenr is 0 wave 1 is active and so on
        spawnWave(); //spawn a wave

        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemies");
        if (totalEnemies.Length == 0)
        {
            if (CurrentWaveNumber != wave.Length)
            {
                if (canAnimate)
                {
                    PauseMenu.Instance.DayCycle.SetFloat("DayNightCycle", -1);
                    waveName.text = wave[CurrentWaveNumber].waveName;
                    Animator.SetTrigger("WaveComplete");
                    canAnimate = false;
                }
            }
            else
            {
                Debug.Log("End Of the Game");
                Animator.SetBool("Win", true);
            }
        }
        if (totalEnemies.Length == 0)
        {
        }
    }

    public void spawnNextWave()
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
            EnemiesSpawned++;

            //print(CurrentWave.NrOfEnemies);

            if (CurrentWave.NrOfEnemies == 0)
            {
                //stop spawning if there's no more enemies in the list
                canSpawn = false;
                canAnimate = true;
            }
        }
    }
}
