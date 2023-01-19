using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider health_slider;

    public void SetHealth(int health){
        health_slider.value = health;
    }

    public void SetMaxHealth(int max_health){
        health_slider.maxValue = max_health;
        health_slider.value = max_health;
    }
}
