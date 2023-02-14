using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyLaserBehavior : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //healthBar = GameObject.FindGameObjectWithTag("Health Bar");

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject x in enemies)
        {
            if (x.GetComponent<Collider>() != null)
            {
                Physics.IgnoreCollision(x.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), true);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            Explode();
        }
    }

    void Explode()
    {
        Destroy(gameObject, .015f);
    }
}
