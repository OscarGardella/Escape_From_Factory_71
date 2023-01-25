using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Upgrade_item : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        //Check if the item collides with an object named "Player"
        if (collision.gameObject.name == "Player")
        {
            Destroy(gameObject);
            upgrading_abilities();
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
        //int num = randomNum(); //random number
        if (gameObject.name == "Upgrade1")
        {

        /*  if (num < 2)
            {
                //move faster
            }
            else if (num < 4)
            {
                //shoot faster
            }
            else if (num < 6)
            {
                //laser damage up
            }
            else if (num < 8)
            {
                //stanima bar max
            }
            else if (num < 10)
            {
                //health +1/4
            } 
        */
        }
    }
}