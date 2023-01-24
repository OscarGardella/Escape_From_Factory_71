using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
// See author attribution in the Attribution.txt file contained in this directory.
// Helpful video: https://www.youtube.com/watch?v=mJu-zdZ9dyE

public class SentryController : MonoBehaviour
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
    
    /* This is a strange problem...
       There is no way to enable, disable, or change the speed of the walking animation.
       For enabling / disabling, I should be able to make an animation for when its idle, and an
       animation for when its walking, and switch between the two. However, because of the strange
       scale properties present in the .fbx animation, disabling or switching away from the animation would
       cause the sprite to become its original scale - way too big. Alternatively, I should be able to change the speed (Window->Animation->Animator, Animator->"SentryRobot|SentryRobotAction 0"->Inspector->Speed)
       property of a certain animation via a parameter. After adding a new float parameter, checking the box for parameter next to the "speed"
       field, and clicking the drop down menu to select my float parameter name, nothing happens, other than that the speed field is
       set to zero, and I cannot control the speed via the script. Therefore, for now, the sprite will always be constantly walking... 
       • TODO before end of last sprint:
         • Try it on a Windows computer to see if its some kind of weird UI or operating system issue. 
         • Ask an online source, such as StackOverflow if this doesn't resolve it.
       Also, it's a really, really terrible walking animation anyways... */

    //animator.speed = agent.velocity.sqrMagnitude;
    //animator.SetFloat("WalkingSpeed", agent.velocity.sqrMagnitude*100);
    //if(agent.velocity.sqrMagnitude < 0.1F) { // Animate if moving
      //animator.enabled = false;
      //animator.SetBool("isWalking", false);

    //} else {
      //animator.enabled = true;
      //animator.SetBool("isWalking", true);
    //}
    
    // Check if close enough to the player to attack
    if(agent.remainingDistance <= agent.stoppingDistance && DateTime.Now > lastAttackCooldown.AddMilliseconds(attackDelayMS)) {
      // TODO: Attack code herre
      //animator.ResetTrigger("AttackTrigger");
      //animator.SetTrigger("AttackTrigger");
      // TODO: I successfuly modified the walk animation into more of an "attack" animation, but there were strange issues when trying to apply and run it,
      // such as needing to modify the component paths in the Animation editor (Window->Animation->Animation), the legs being scrunched up into the body, 
      // sprite scale problems, or the new animation not playing at all.
    }
  }
}
