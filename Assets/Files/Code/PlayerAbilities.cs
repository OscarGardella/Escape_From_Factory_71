using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public PowerBar player_power_bar;
    public AbilityTracking abilityTracker;

    // Update is called once per frame
    void Update()
    {
        if (abilityTracker.IsAbilityUnlocked(AbilityTracking.AbilityName.Roll))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) //roll ability key
            {
                player_power_bar.ReducePower(5); //depends on skill cost
                Debug.Log("Autobots roll out!");
            }
        }
    }
}