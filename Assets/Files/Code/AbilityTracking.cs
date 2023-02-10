using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityTracking : MonoBehaviour
{
    public Image activeIcon1;
    public Image activeIcon2;
    public Sprite[] spriteArray;

    public enum AbilityName //names of all abilites
    {
        Beam, Blackhole, Roll, Rock, Heal
    }

    private List<AbilityName> unlockedAbilityList;

    public AbilityTracking(){
        unlockedAbilityList = new List<AbilityName>();
    }

    public void UnlockAbility (AbilityName unlockedAbility){ //adds ability to the unlocked set
        if (IsAbilityUnlocked(unlockedAbility) == false) //checks if ability is already unlocked
        {
            unlockedAbilityList.Add(unlockedAbility); //adds ability to the list of unlocked abilites
            if (activeIcon1.gameObject.activeSelf == false){ //if you've already unlocked an ability move your second one to slot two
                RevealAbility(unlockedAbility, activeIcon1);
            } else {
                RevealAbility(unlockedAbility, activeIcon2);
            }
        }
    }

    public void RevealAbility (AbilityName unlockedAbility, Image icon){
        if (Equals(unlockedAbility, AbilityName.Beam)){
            icon.sprite = spriteArray[0];
            icon.gameObject.SetActive(true);
        } else if (Equals(unlockedAbility,AbilityName.Blackhole)){
            icon.sprite = spriteArray[1];
            icon.gameObject.SetActive(true);
        } else if (Equals(unlockedAbility, AbilityName.Roll)){
            icon.sprite = spriteArray[2];
            icon.gameObject.SetActive(true);
        } else if (Equals(unlockedAbility,AbilityName.Rock)){
            icon.sprite = spriteArray[3];
            icon.gameObject.SetActive(true);
        } else if (Equals(unlockedAbility,AbilityName.Heal)){
            icon.sprite = spriteArray[4];
            icon.gameObject.SetActive(true);
        }
    }

    public bool IsAbilityUnlocked (AbilityName ability){ //returns true if ability is in set 
        return unlockedAbilityList.Contains(ability);
    }

}
