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
        if(! player) Debug.Log("enemyPewPew.cs: error: Failed to find a game object with tag Player");
        foreach (Transform t in transform) {
            t.gameObject.tag = "Enemy";
        }

        hack = GameObject.FindGameObjectWithTag("Hack");
        if(! hack) Debug.Log("enemyPewPew.cs: Error: Failed to find a game object with tag Hack. This script will probably crash");

        hacktext = hack.GetComponent<OpeningAnimHackText>();
        if(! hacktext) Debug.Log("enemyPewPew.cs: Error: Failed to find component OpeningAnimHackText. This script will probably crash");

        GameObject hitbox = GameObject.FindGameObjectWithTag("Hitbox");
        if(! hitbox) Debug.Log("enemyPewPew.cs: Error: Failed to find a game object with tag Hitbox");
        hitboxLayer = hitbox.layer;
    }
    void Update()
    {
        
        if (canPlay == true){
        } else if (hacktext.hasPlayed() == true){
            canPlay = true;
        }
        
        transform.LookAt(player.transform);

        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < range)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
        
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
            if (coolDown < 0)  {
                coolDown = 0;
            }
        }
        
        if (Physics.Raycast(transform.position, transform.position-player.transform.position, hitboxLayer)){
            if (inRange == true && coolDown == 0 && canPlay == true) {
                Shoot();
            }
        }
        
        if (coolDown > 0) {
            coolDown -= Time.deltaTime;
            if (coolDown < 0) {
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
        
        if(AudioManager.Instance) { 
            AudioManager.Instance.PlaySFX("Enemy Shooting");
        } else {
            Debug.Log("enemyPewPew.cs: Error: AudioManager.instance is null! Cannot play enemy shooting sound.");
        }
        Destroy(laser, 3f);
        
    }

}