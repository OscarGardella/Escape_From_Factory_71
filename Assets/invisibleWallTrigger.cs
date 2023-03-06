using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class invisibleWallTrigger : MonoBehaviour
{
    public GameObject InvisibleWall;
    private int count = 0;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && count == 0)
        {
            count = 1;
            InvisibleWall.SetActive(true);
        }
    }
}
