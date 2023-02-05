using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWave : MonoBehaviour
{

    public void Spawn(){
        //wordt gereferenced in de animator
        waveSpawner.Instance.spawnNextWave();
    }
}
