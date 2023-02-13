using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitWallCollision : MonoBehaviour
{
    //private bool isCollided;

    //public bool IsCollided
    //{
    //    get { return isCollided; }
    //}
    public GameSuccessCanvas SuccessScreen;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag== "Player")
        {
            SuccessScreen.showUI();
            //isCollided = true;
        }
    }
}
