using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class pewPew : MonoBehaviour

{
    private GameObject hack;

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float launchVelocity = 3000f;
    [SerializeField]
    private AudioSource sfx;
    public double FireDelay = 1.0;
    private Vector3 mousePos;
    private double coolDown = 0;
    [SerializeField]
    private PowerBar powerBar;
    public float PowerDrain = 1.0f;
    [SerializeField]
    private Camera camera;
    private OpeningAnimHackText hacktext;
    private bool canPlay = false;

    [HideInInspector]
    public bool Enabled = true;
    public float Duration = 3f;
    private Quaternion defRot;
    // Start is called before the first frame update
    void Start()
    {
        hack = GameObject.FindGameObjectWithTag("Hack");
        hacktext = hack.GetComponent<OpeningAnimHackText>();
        defRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {   //Unity Forum Go Crazy
        if (canPlay == true){
        } else if (hacktext.hasPlayed() == true){
            canPlay = true;
        }

        if (coolDown > 0){
            coolDown -= Time.deltaTime;
            if (coolDown < 0){
                coolDown = 0;
            }
        }
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;

        if (plane.Raycast(ray, out distance)) {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg - 180;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
        bool canShoot = (enabled == true && coolDown == 0 && pauseGame.paused == false && canPlay == true);
        if (Input.GetMouseButton(0) && canShoot) {
            shoot();
        }
        else if (Input.GetKey(KeyCode.UpArrow)&&Input.GetKey(KeyCode.RightArrow)&& canShoot)    //Forward&Right
        {
            transform.rotation = Quaternion.Euler(-90, 0, 45);
            transform.Rotate(-90, 0, 0);
            shoot();
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow) && canShoot)    //Forward&Left
        {
            transform.rotation = Quaternion.Euler(-90, 0, -45);
            transform.Rotate(-90, 0, 0);
            shoot();
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow) && canShoot)    //Back and Right
        {
            transform.rotation = Quaternion.Euler(-90, 0, 135);
            transform.Rotate(-90, 0, 0);
            shoot();
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow) && canShoot)    //Back&Left
        {
            transform.rotation = Quaternion.Euler(-90, 0, -135);
            transform.Rotate(-90, 0, 0);
            shoot();
        }
        else if (Input.GetKey(KeyCode.UpArrow)&&canShoot)   //Shoots Up
        {
            transform.rotation = Quaternion.Euler(-90, 0, 0);
            transform.Rotate(-90, 0, 0);
            shoot();
        }
        else if (Input.GetKey(KeyCode.DownArrow) && canShoot)   //Shoots Down
        {
            transform.rotation = Quaternion.Euler(-90, 0, 180);
            transform.Rotate(-90, 0, 0);
            shoot();
        }
        else if (Input.GetKey(KeyCode.RightArrow) && canShoot)   //Shoots Right
        {
            transform.rotation = Quaternion.Euler(-90, 0, 90);
            transform.Rotate(-90, 0, 0);
            shoot();
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && canShoot)   //Shoots Left
        {
            transform.rotation = Quaternion.Euler(-90, 0, -90);
            transform.Rotate(-90, 0, 0);
            shoot();
        }
    }

    private void shoot()
    {

        if (powerBar.ReducePower(PowerDrain) == true)
        {
            coolDown += FireDelay;
            GameObject laser = Instantiate(projectile, transform.position, transform.rotation);

            laser.transform.Rotate(90, 0, 0);

            laser.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(90, 0, -launchVelocity));

            laser.transform.Translate(0, (float)(1/-1200.0)* (float)(launchVelocity), 0);
            AudioManager.Instance.PlaySFX("Player Shooting");
            Destroy(laser, Duration);
        }

    }

}
