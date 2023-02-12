using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemyPewPew : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float launchVelocity = 300f;
    [SerializeField]
    private AudioSource sfx;
    public double fireDelay = 5.0;
    private Vector3 mousePos;
    private double coolDown = 0;
    private GameObject player = null;
    private bool inRange = false;
    [SerializeField]
    private double range = 20;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (Transform t in transform)
        {
            t.gameObject.tag = "Enemy";
        }
    }
    void Update()
    {
        transform.LookAt(player.transform);

        if (inRange == false){
            float dist = Vector3.Distance(player.transform.position, transform.position);
            if (dist < range){
                inRange = true;
            }
        }
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
            if (coolDown < 0)
            {
                coolDown = 0;
            }
        }


        if (inRange==true && coolDown == 0)
        {

            Shoot();
        }  
    }
    void Shoot()
    {
        coolDown += fireDelay;
        GameObject laser = Instantiate(projectile, transform.position, transform.rotation);
        laser.transform.Rotate(90, 0, 0);
        laser.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, launchVelocity));
        AudioManager.Instance.PlaySX("Enemy Shooting");
        Destroy(laser, 3f);
    }

}