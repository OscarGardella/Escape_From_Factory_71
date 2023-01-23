using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      GameObject player = GameObject.FindGameObjectWithTag("Player");
      Physics.IgnoreCollision(player.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), true);
      Destroy(gameObject, 3);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            
            Destroy(gameObject);
            Explode();
        }
    }

    void Explode()
    {
        var exp = GetComponent<ParticleSystem>();
        exp.Play();
    }
}
