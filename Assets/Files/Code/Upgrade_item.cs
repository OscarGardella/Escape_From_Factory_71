using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Upgrade_item : MonoBehaviour
{
    public Upgrade_Choice upgradePanel1;
    public Upgrade_Choice upgradePanel2;

    void OnCollisionEnter(Collision collision)
    {
        //Check if the item collides with an object named "Player"
        if (collision.gameObject.name == "MainCharacter")
        {
            upgradePanel1.revealUpgrade();
            upgradePanel2.revealUpgrade();
            Destroy(gameObject);
        }
    }

    //this fucntion is only used for testing, remove before release
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) //roll ability key
        {
            upgradePanel1.revealUpgrade();
            upgradePanel2.revealUpgrade();
        }
    }
}