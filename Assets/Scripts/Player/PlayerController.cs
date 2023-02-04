using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public Transform Player;

    void Awake() => Instance = this;

    // Update is called once per frame
    void Update()
    {

    }
}
