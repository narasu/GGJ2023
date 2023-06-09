using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public Transform player;
    public float currency = 10;
    public TextMeshProUGUI currencyAmount, noFundsWarning;


    void Awake() => Instance = this;
    
    private void Update() {
        currencyAmount.text = currency.ToString();
    }

}
