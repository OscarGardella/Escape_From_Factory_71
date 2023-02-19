using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBehavior : MonoBehaviour { 

    private ScoreKeeper score;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreCollision(player.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), true);
        Destroy(gameObject, 3);
        score = GameObject.FindGameObjectWithTag("ScoreDisplay").GetComponent<ScoreKeeper>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            score.incrScore(100);
            Explode();
        }
        else if (collision.gameObject.tag != "Player")
        {

            // Destroy(collision.gameObject);
            Explode();
        }
    }

    void Explode()
    {
        //var exp = GetComponent<ParticleSystem>();
        //exp.Play();
        Destroy(gameObject);
    }
}
