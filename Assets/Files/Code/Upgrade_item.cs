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

    // Update is called once per frame
    void Update()
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



    int randomNum()
    {
        System.Random rd = new System.Random();
        int num = rd.Next(0, 10);
        Console.WriteLine(num);
        return num;
    }

    //public static void Main()
    //{
        // Instantiate random number generator
    //    Random rand = new Random();
     //   int num = rand.Next(0, 10);
    //}

    void upgrading_abilities()
    {
        int num = randomNum(); //random number
        if (gameObject.name == "Upgrade1")
        {
            if (num < 2)
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
        }
        else if (gameObject.name == "Upgrade2")
        {
            if (num < 2)
            {
                //less damage from attack
            }
            else if (num < 4)
            {
                //widen laser reach range
            }
            else if (num < 7)
            {
                //health +1/2
            }
            else if (num < 10)
            {
                //stun EMP
            }

        }
        else if (gameObject.name == "Upgrade3")
        {
            if (num < 3)
            {
                //health max
            }
            else if (num < 6)
            {
                // special move
            }
            else if (num < 10)
            {
                //stamina bar recovery rate up
            }

        }
    }
}