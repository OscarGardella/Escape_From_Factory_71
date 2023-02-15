using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public PowerBar player_power_bar;
    public AbilityTracking abilityTracker;
    public RobotFreeAnim player_character;

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Q)){
            ActivateAbility(abilityTracker.GetQAbility());
        }
        if (Input.GetKeyDown(KeyCode.E)){
            ActivateAbility(abilityTracker.GetEAbility());
        }
    }

    void ActivateAbility(AbilityTracking.AbilityName ability){ 
        if(ability == AbilityTracking.AbilityName.Beam){ //use beam ability
            if(player_power_bar.ReducePower(7) == true){
                //beam method call
            }
        } if(ability == AbilityTracking.AbilityName.Blackhole){ //use blackhole ability
            if(player_power_bar.ReducePower(10) == true){
                //blackhole method call
            }
        } else if(ability == AbilityTracking.AbilityName.Roll){ //use roll ability
            if(player_power_bar.ReducePower(4) == true){
                player_character.rollFor(2.0f);
            }
        }
    }
}