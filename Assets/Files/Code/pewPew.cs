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
    public double fireDelay = 1.0;
    private Vector3 mousePos;
    private double coolDown = 0;
    [SerializeField]
    private PowerBar powerBar;
    public float powerDrain = 1.0f;
    [SerializeField]
    private Camera camera;
    private OpeningAnimHackText hacktext;
    // Start is called before the first frame update
    void Start()
    {

        hack = GameObject.FindGameObjectWithTag("Hack");
        hacktext = hack.GetComponent<OpeningAnimHackText>();
    }

    // Update is called once per frame
    void Update()
    {   //Unity Forum Go Crazy
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
            if (coolDown < 0)
            {
                coolDown = 0;
            }
        }
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg - 180;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }

        if (Input.GetMouseButton(0) && coolDown == 0 && pauseGame.paused == false && hacktext.hasPlayed() == true)
        {
            shoot();
        }
    }

    private void shoot()
    {

        if (powerBar.ReducePower(powerDrain) == true)
        {
            coolDown += fireDelay;
            GameObject laser = Instantiate(projectile, transform.position, transform.rotation);

            laser.transform.Rotate(90, 0, 0);

            laser.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(90, 0, -launchVelocity));

            laser.transform.Translate(0, (float)(1/60.0)* (float)(1.0/-20*launchVelocity), 0);
            AudioManager.Instance.PlaySFX("Player Shooting");
            Destroy(laser, 3f);
        }

    }

}
