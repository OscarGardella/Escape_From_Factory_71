using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ExitWallCollision : MonoBehaviour
{
    public GameSuccessCanvas SuccessScreen;

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("oaoaoa");
        if (other.gameObject.tag == "Player")
        {
            SuccessScreen.ShowUI();
        }
    }

}
