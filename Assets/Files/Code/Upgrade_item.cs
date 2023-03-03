using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Upgrade_item : MonoBehaviour
{
    public Upgrade_Choice upgradePanel1;
    public Upgrade_Choice upgradePanel2;

    void OnCollisionEnter(Collision collision){
        //Check if the item collides with an object named "Player"
        if (collision.gameObject.name == "MainCharacter"){
            int arrayLength = upgradePanel1.makePanelArray().Length;
            int upgrade1 = randomNum(0, arrayLength);
            int upgrade2 = randomNum(0, arrayLength);
            if (upgrade1 == upgrade2){ //stops the panels revealing the same upgrade
                if(upgrade2 < arrayLength){ //will have to be -1
                    upgrade2++;
                } else {
                    upgrade2--;
                }
            }
            upgradePanel1.revealUpgrade(upgrade1);
            upgradePanel2.revealUpgrade(upgrade2);
            Destroy(gameObject);
        }
    }

    int randomNum(int low, int high){ //makes random number from low to high
        System.Random rd = new System.Random();
        int num = rd.Next(low, high);
        return num;
    }

    //this fucntion is only used for testing, remove before release
    void Update(){
        transform.Rotate(Time.deltaTime * 0, 15, 0);
    }
}