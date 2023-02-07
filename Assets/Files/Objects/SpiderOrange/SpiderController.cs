using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
// See author attribution in the Attribution.txt file contained in this directory.
// Helpful video: https://www.youtube.com/watch?v=mJu-zdZ9dyE

public class SpiderController : MonoBehaviour
{
  private NavMeshAgent agent;
  private Animator animator;
  public string tagSearchName; // This will only be used if target is not set.
  private GameObject target = null;
  private DateTime lastAttackCooldown;
  public int attackDelayMS; // Attack delay, in milliseconds
  public PlayerHealth healthBar;
  public float activationDistance = 10; // The enemy will wait until the player is within this range to start following.

  // Start is called before the first frame update
  void Start() {
    agent = this.gameObject.GetComponent<NavMeshAgent>();
    animator = this.gameObject.GetComponent<Animator>();
    healthBar = GameObject.FindGameObjectWithTag("Health").GetComponent<PlayerHealth>();
    if(! healthBar) {
      Debug.Log("SpiderController.cs: Error: unable to find HealthBar via tag \"Health\"");
    }
    lastAttackCooldown = DateTime.Now;
    target = GameObject.FindGameObjectWithTag("Player"); // Attempt to find the MainCharacter if target is not set
    if(target == null) {
      Debug.Log("Error: Enemy \"" + this.name + "\" is not set to follow any target");
      //Debug.Log("Error: Enemy \"" + this.name + "\" was unable to find the target \"" + target.name + "\". Navigation script will be disabled.");
      agent.enabled = false; // Disable navmesh agent
    }

    lastAttackCooldown = DateTime.Now.AddMilliseconds(attackDelayMS);
    agent.enabled = false; // Disabled until within activation distance
  }

  // Update is called once per frame
  void Update() {
    
    // Wait until the player is within activationDistance range of the player
    if(agent.enabled == false) {
      if(Vector3.Distance(gameObject.transform.position, target.transform.position) <= activationDistance) {
        agent.enabled = true; // Activate movement
      }
      return;
    }
    agent.SetDestination(target.transform.position);
    
    if(agent.velocity.sqrMagnitude < 0.1F) { // Animate if moving
      animator.SetBool("isWalking", false);

    } else {
      animator.SetBool("isWalking", true);
    }
    
    // Check if close enough to the player to attack
    if(agent.remainingDistance <= agent.stoppingDistance && Vector3.Distance(gameObject.transform.position, target.transform.position) <= agent.stoppingDistance && DateTime.Now > lastAttackCooldown.AddMilliseconds(attackDelayMS)) {
      lastAttackCooldown = DateTime.Now;
      animator.ResetTrigger("AttackTrigger");
      animator.SetTrigger("AttackTrigger");
      if(healthBar) {
        healthBar.TakeDamage(1);
      }
    }
  }
}
