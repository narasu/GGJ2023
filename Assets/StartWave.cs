using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWave : MonoBehaviour
{
    public AudioClip Audio1;
    public AudioClip Audio2;
    public AudioSource PlayAudio;
    //public FMOD.Studio.EventInstance fmodInstance;
    

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
        PlayAudio.clip = Audio1;
        PlayAudio.Play();
    }
    public void SoundEffect2(){
        PlayAudio.clip = Audio2;
        PlayAudio.Play();
    }
}
