using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    
    public float max_power = 50;
    public float current_power;
    public float regen_rate = 1;
    public Slider power_slider;

    public void SetPower(float power){
        power_slider.value = power;
    }

    public void SetMaxPower(float max_power){
        power_slider.maxValue = max_power;
        power_slider.value = max_power;
    }

    void Start()
    {
        current_power = max_power;
        SetMaxPower(max_power);
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ReducePower(5); //depends on skill cost
        }
        if (current_power < max_power)
        {
            current_power += regen_rate * Time.deltaTime;
            SetPower(current_power);
            if (current_power > max_power)
            {
                current_power = max_power;
            }
        }
    }

    public bool ReducePower(float power_cost)
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
