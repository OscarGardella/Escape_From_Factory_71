using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add power cost section to abilities (use tuples?)

public class AbilityTracking : MonoBehaviour
{
    public enum AbilityName //names of all abilites
    {
        Roll, EMP, Explode
    }

    private List<AbilityName> unlockedAbilityList;

    public AbilityTracking()
    {
        unlockedAbilityList = new List<AbilityName>();
    }

    public void UnlockAbility (AbilityName unlockedAbility) //adds ability to the unlocked set
    {
        if (IsAbilityUnlocked(unlockedAbility) == false) //checks if ability is already unlocked
        {
            unlockedAbilityList.Add(unlockedAbility); //adds ability to the list of unlocked abilites
        }
    }

    public bool IsAbilityUnlocked (AbilityName ability) //returns true if ability is in set 
    {
        return unlockedAbilityList.Contains(ability);
    }

}
