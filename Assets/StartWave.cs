using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWave : MonoBehaviour
{
    public AudioSource Audio1;
    public AudioSource Audio2;
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

    public void SoundEffect1(){
//        Audio2.Play();
    }
    public void SoundEffect2(){
   //     Audio1.Play();
    }
}
