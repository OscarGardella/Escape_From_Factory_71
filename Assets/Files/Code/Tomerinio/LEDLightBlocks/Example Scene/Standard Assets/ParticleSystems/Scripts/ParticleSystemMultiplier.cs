using System;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
    public class ParticleSystemMultiplier : MonoBehaviour
    {
        // a simple script to scale the size, speed and lifetime of a particle system

        public float multiplier = 1;
        public ParticleSystem systems;


        public void Start()
        {
            //var ps = GetComponentsInChildren<ParticleSystem>();
            // foreach (ParticleSystem system in systems)
            // {
            //    system.startSize *= multiplier;
            //    system.startSpeed *= multiplier;
            //    system.startLifetime *= Mathf.Lerp(multiplier, 1, 0.5f);
            //     system.Clear();
            //    system.Play();
            //}

            Component[] systems;
            systems = GetComponentsInChildren(typeof(ParticleSystem)); //Returns all components of ParticleSystem in the GameObject or any of its children
            

            foreach (ParticleSystem system in systems)
            {
                var main = system.main;
                //system.startSize *= multiplier;//the initial size that should be used for a particle.
                //ParticleSystem.MainModule main = systems.main;
                ParticleSystem.MinMaxCurve minMaxCurve = main.startSize; //Get Size
                //minMaxCurve.constant *= multiplier; //Modify Size
                main.startSize = multiplier;  //Assign the modified startSize back

                //system.startSpeed *= multiplier;//the initial speed that should be used for a particle.
                //ParticleSystem.MainModule main = system.main;
                ParticleSystem.MinMaxCurve minMaxCurve2 = main.startSpeed; //Get Speed
                //minMaxCurve.constant *= multiplier; //Modify Speed
                main.startSpeed = multiplier; //Assign the modified startSpeed back

                //system.startLifetime *= Mathf.Lerp(multiplier, 1, 0.5f);
                main.startLifetime = multiplier;
                multiplier = Mathf.Lerp(multiplier, 1, 0.5f);
                system.Clear();
                system.Play();
            }
        }

    }
}
