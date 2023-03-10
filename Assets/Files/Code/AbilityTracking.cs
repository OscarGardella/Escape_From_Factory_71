using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityTracking : MonoBehaviour
{
    public Image activeIcon1;
    public Image activeIcon2;
    public TMP_Text tutorial;
    public Sprite[] spriteArray;
    public AbilityName QAbility;
    public AbilityName EAbility;
    public PlayerAbilities player;

    public enum AbilityName //names of all abilites
    {
        Beam, Shield, Roll, Target, Heal, Energy, Speed, Null
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
        GainPassive(unlockedAbility); //call for all passive abilities
        if (icon == activeIcon1){ //sets key bindings
            QAbility = unlockedAbility;
        } else {
            EAbility = unlockedAbility;
        }
        if (Equals(unlockedAbility, AbilityName.Beam)){
            icon.sprite = spriteArray[0];
            icon.gameObject.SetActive(true);
            Tutorial(icon);
        } else if (Equals(unlockedAbility, AbilityName.Shield)){
            icon.sprite = spriteArray[1];
            icon.gameObject.SetActive(true);
            Tutorial(icon);
        } else if (Equals(unlockedAbility, AbilityName.Roll)){
            icon.sprite = spriteArray[2];
            icon.gameObject.SetActive(true);
            Tutorial(icon);
        }
    }

    public void GainPassive(AbilityName ability){ //if it is a passive ability it unlocks it, otherwise does nothing
        player.PassiveAbility(ability);
    }

    public void Tutorial(Image icon){ //reveals text explaining what button to click to activate your ability
        if(icon == activeIcon1){
            tutorial.text = "Click Q to activate your new ability!"; //may want to add ability name to tutorial in the future
            StartCoroutine(RevealTutorial());
        } else {
            tutorial.text = "Click E to activate your new ability!";
            StartCoroutine(RevealTutorial());
        }
    }

    public void NoAbility(){ //used to tell the player they haven't unlocked an ability
        tutorial.text = "You do not have an ability unlocked for that button";
        StartCoroutine(RevealTutorial());
    }

    IEnumerator RevealTutorial(){ //reveals tutorial for a few seconds
        //code based from stack overflow
        tutorial.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        tutorial.gameObject.SetActive(false);
    }

    public bool IsAbilityUnlocked (AbilityName ability){ //returns true if ability is in set
        return unlockedAbilityList.Contains(ability);
    }

    public AbilityName GetQAbility(){
        return QAbility;
    }

    public AbilityName GetEAbility(){
        return EAbility;
    }

    void Start(){
        activeIcon1.gameObject.SetActive(false);
        activeIcon2.gameObject.SetActive(false);
        QAbility = AbilityName.Null;
        EAbility = AbilityName.Null;
        tutorial.gameObject.SetActive(false);
    }
}
