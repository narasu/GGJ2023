using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public static DayCycle Instance;
    public bool isNight = true;

    private void Awake()
    {
        Instance = this;
    }
}

