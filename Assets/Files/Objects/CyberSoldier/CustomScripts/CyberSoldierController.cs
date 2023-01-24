using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
// See author attribution in the Attribution.txt file contained in this directory.
// Helpful video: https://www.youtube.com/watch?v=mJu-zdZ9dyE

public class CyberSoldierController : MonoBehaviour
{
  private NavMeshAgent agent;
  private Animator animator;
  public string targetSearchName; // This will only be used if target is not set.
  public GameObject target;
  private DateTime lastAttackCooldown;
  public int attackDelayMS; // Attack delay, in milliseconds

  // Start is called before the first frame update
  void Start() {
    agent = this.gameObject.GetComponent<NavMeshAgent>();
    animator = this.gameObject.GetComponent<Animator>();
    lastAttackCooldown = DateTime.Now;
    if(target == null) target = GameObject.Find(targetSearchName); // Attempt to find the MainCharacter if target is not set
    if(target == null) {
      Debug.Log("Error: Enemy \"" + this.name + "\" is not set to follow any target");
      //Debug.Log("Error: Enemy \"" + this.name + "\" was unable to find the target \"" + target.name + "\". Navigation script will be disabled.");
      enabled = false; // Disable this script.
    }
  }

  // Update is called once per frame
  void Update() {
    agent.SetDestination(target.transform.position);
    
    if(agent.velocity.sqrMagnitude < 0.1F) { // Animate if moving
      animator.SetBool("isWalking", false);

    } else {
      animator.SetBool("isWalking", true);
    }
    
    // Check if close enough to the player to attack
    if(agent.remainingDistance <= agent.stoppingDistance && DateTime.Now > lastAttackCooldown.AddMilliseconds(attackDelayMS)) {
      // TODO: Attacking mechanics
      //animator.ResetTrigger("AttackTrigger");
      //animator.SetTrigger("AttackTrigger");
    }
  }
}
