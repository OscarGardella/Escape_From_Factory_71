using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Upgrade_item : MonoBehaviour
{
    public AbilityTracking abilityTracker;
    // Start is called before the first frame update
    void Start()
    {
        //write code to instantiate ability ID for each upgrade pickup
    }

    void OnCollisionEnter(Collision collision)
    {
        //Check if the item collides with an object named "Player"
        if (collision.gameObject.name == "MainCharacter")
        {
            upgrading_abilities();
            Destroy(gameObject);
        }
    }

    int randomNum() //makes random number from 0-10
    {
        System.Random rd = new System.Random();
        int num = rd.Next(0, 10);
        Console.WriteLine(num);
        return num;
    }

    void upgrading_abilities()
    {
        if (gameObject.name == "Upgrade1")
        {
            abilityTracker.UnlockAbility(AbilityTracking.AbilityName.Roll); //at somepoint will add ability based on id
        }
    }
}