using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public static DayCycle Instance;
    public bool isNight = true;

    private void Awake() {
        Instance = this;
    }

    public void Night(){
        if(isNight){
            //reference player controller script om movement toe te staan
            PauseMenu.Instance.StartNight();
        }
    }

    public void Day(){
        if (!isNight){
            //reference player script zodat player niet meer kan bewegen en enemy spawn begint.
            
        }
    }

}
