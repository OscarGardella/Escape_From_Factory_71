using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void UnlockAbility (AbilityName unlockedability) //adds ability to the unlocked set
    {
        unlockedAbilityList.Add(AbilityName);
    }

    public bool IsAbilityUnlocked (AbilityName ability) //returns true if ability is in set 
    {
        return unlockedAbilityList.Contains(ability);
    }

}
