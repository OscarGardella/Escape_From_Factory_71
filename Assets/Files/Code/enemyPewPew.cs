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

    private GameObject hack;
    private OpeningAnimHackText hacktext;
    private bool canPlay = false;
    RaycastHit hit;
    private LayerMask hitboxLayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (Transform t in transform)
        {
            t.gameObject.tag = "Enemy";
        }
        hack = GameObject.FindGameObjectWithTag("Hack");
        hacktext = hack.GetComponent<OpeningAnimHackText>();

        GameObject hitbox = GameObject.FindGameObjectWithTag("Hitbox");
        hitboxLayer = hitbox.layer;
    }
    void Update()
    {

        if (canPlay == true){
        }
        else if (hacktext.hasPlayed() == true)
        {
            canPlay = true;
        }
        transform.LookAt(player.transform);

        if (inRange == false)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            if (dist < range)
            {
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
        if (Physics.Raycast(transform.position, transform.position-player.transform.position, hitboxLayer))
        {
            if (inRange == true && coolDown == 0 && canPlay == true) {
                Shoot();
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



    }
    void Shoot()
    {
        coolDown += fireDelay;
        GameObject laser = Instantiate(projectile, transform.position, transform.rotation);
        laser.transform.Rotate(90, 0, 0);
        laser.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, launchVelocity));
        AudioManager.Instance.PlaySFX("Enemy Shooting");
        Destroy(laser, 3f);
    }

}