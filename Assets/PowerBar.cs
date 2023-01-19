using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public int max_power = 50;
    public int current_power;
    public Slider power_slider;
    //public PowerBar player_power_bar;

    public void SetPower(int power){
        power_slider.value = power;
    }

    public void SetMaxPower(int max_power){
        power_slider.maxValue = max_power;
        power_slider.value = max_power;
    }

    void Start()
    {
        current_power = max_power;
        SetMaxPower(max_power);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ReducePower(5); //depends on skill cost
        }
    }

    bool ReducePower(int power_cost)
    {
        if(current_power - power_cost < 0) {
            return(false);
        } else {
            current_power -= power_cost;
            SetPower(current_power);
            return(true);
        }
    }
}
