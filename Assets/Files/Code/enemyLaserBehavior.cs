using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyLaserBehavior : MonoBehaviour
{
    private GameObject healthBar=null;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        var healthBar = GameObject.Find("Health Bar");

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject x in enemies){
            if (x.GetComponent<Collider>() != null)
            {
                Physics.IgnoreCollision(x.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), true);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player"&& collision.gameObject.tag != "Enemy")
        {
            Explode();
        }
        else if (collision.gameObject.tag=="Player)")
        {
            Destroy(gameObject, 3f);
           }
           else{
            Explode();
           }
    }

    void Explode()
    {
        Destroy(gameObject);
    }
}
