using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //this script is for the healthbar, once the player health gets to 1/3 of the maxhealth the bar will turn red
    public PlayerHealth playerHealth;
    public Image fillImage;
    private Slider slider;
    // Start is called before the first frame update
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false; //if the health is empty, the slider is turned off. this is purely aesthetic.
        }

        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = enabled; //if there is health the slider is enabled
        }

        float fillValue = playerHealth.currentHealth / playerHealth.MaxHealth; //fill value is calculated using current health and maxhealth
        if (fillValue <= slider.maxValue / 3)
        {
            fillImage.color = new Color(.53f, .11f, .15f); //health in critical condition (red).

        }
        else if (fillValue > slider.maxValue / 3)
        {
            fillImage.color = new Color(0f, .8f, .12f); // health normal condition (green)
        }

        slider.value = fillValue; //slider value is set to calculated health value.
    }
}
