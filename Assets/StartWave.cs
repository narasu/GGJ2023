using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWave : MonoBehaviour
{
    public AudioClip Audio1;
    public AudioClip Audio2;
    public AudioSource PlayAudio;
    public FMODUnity.EventReference dag;
    public FMODUnity.EventReference nacht;

    private FMOD.Studio.EventInstance _fmodInstance;


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
        SoundEffect2();
    }

    public void SoundEffect1()
    {
        _fmodInstance = FMODUnity.RuntimeManager.CreateInstance(dag);
        _fmodInstance.start();
        _fmodInstance.release();

    }
    public void SoundEffect2()
    {
        _fmodInstance = FMODUnity.RuntimeManager.CreateInstance(nacht);
        _fmodInstance.start();
        _fmodInstance.release();
    }
}
