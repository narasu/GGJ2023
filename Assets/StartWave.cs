using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWave : MonoBehaviour
{

    public void Spawn()
    {
        //wordt gereferenced in de animator
        waveSpawner.Instance.spawnNextWave();
    }

    public void EndTurnEnem()
    {
        //start daycycle in anim
        DayCycle.Instance.isNight = true;
        PauseMenu.Instance.StartNight();
    }
}
