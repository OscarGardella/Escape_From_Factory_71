using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Upgrade_item : MonoBehaviour
{
    public Upgrade_Choice upgradeChooser;

    void OnCollisionEnter(Collision collision)
    {
        //Check if the item collides with an object named "Player"
        if (collision.gameObject.name == "MainCharacter")
        {
            upgradeChooser.selectUpgrade();
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) //roll ability key
        {
            upgradeChooser.selectUpgrade();
        }
    }
}