using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* InputControls class, by Justin Douty
   This class acts as a controller for a character. It currenly provides whether the character should be moving,
   along with a desired rotation indicated by the controls.
*/
public class InputControls //: MonoBehaviour
{
  // Start is called before the first frame update

  public float globalOrientation = 0; // Which way the controls should act as if the player was facing
  public bool currMoving = false;
  public ControlMode controlMode = ControlMode.WASD;
  public bool controlsEnabled = true;

  void Start() {
    
  }
  
  public enum ControlMode { WASD, ArrowKeys };

  // Returns whether any key is pressed corresponding to character movement
  private bool movementKeyPressed() {
    switch(controlMode) {
      case ControlMode.WASD:
        return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S);
      case ControlMode.ArrowKeys:
        return Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow);
      default:
        return false;
    }

  }
  // Returns whether any key is held corresponding to character movement
  private bool movementKeyHeld() {
    switch(controlMode) {
      case ControlMode.WASD:
        return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S);
      case ControlMode.ArrowKeys:
        return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow);
      default:
        return false;  
    }
  }
  // Returns whether any movement key has been released
  private bool movementKeyReleased() {
    switch(controlMode) {
      case ControlMode.WASD:
        return Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S);
      case ControlMode.ArrowKeys:
        return Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow);
      default:
        return false;
    }
  }
  // Returns whether all movement keys were released (the character should not be moving)
  private bool allMovementKeysReleased() {
    return movementKeyReleased() && ! movementKeyHeld();
  }
  

  // These are the public-facing API functions. These can return alternate values based on specified control modes, or whatever specified!
  public bool startedMoving() { // If input is given that the character should start moving. This should return true only on that first frame.
    if(! controlsEnabled) return false;
    if(currMoving == false) {
      currMoving = movementKeyPressed();
      return currMoving;
    }
    return false;   
  }

  public bool stoppedMoving() { // Returns true only on the first frame that signals that the character should stop moving
    if(! controlsEnabled) return false;
    if(currMoving && allMovementKeysReleased()) {
      currMoving = false;
      return true;
    }
    return false;
  }

  public bool isMoving() { // Returns true while the character should be moving
    return currMoving && controlsEnabled; //movementKeyHeld();
  }

  // Gets the angle the player should currently be facing, based on the direction it is currently facing, and the controls alone
  public float getFacing(float playerRot) {
    if(!controlsEnabled || ! isMoving()) return playerRot; // If no input given, hold current angle
    float target = getKeypadAngle();
    return getNearestAngle(playerRot, target, globalOrientation);
  }

  // Returns the angle that the character would have to turn the shortest distance to face, given an arbitrary angle.
  // Finds the upper and lowermost multiples of 360 closest to currAngle. Then, decides if it is closer to move closer to the lower or upper reference.
  // <param name="currAngle">the current angle something is facing</param>
  // <param name="targetAngle">the angle something wants to face</param>
  // <param name="orientation">a global rotation offset (ex: if up for your character really means 270 degrees instead of 0)</param>
  public static float getNearestAngle(float currAngle, float targetAngle, float orientation) {
    currAngle = currAngle - orientation;
    float adjTarget = (targetAngle + orientation) % 360;
    int offset = 1;
    if(currAngle < 0) offset = 0;
    float upperReference = 360 * (offset + (int) (currAngle / 360)); // The uppermost multiple of 360 that currAngle is close to
    float lowerReference = upperReference - 360; // The lowermost multiple of 360

    // Edge case - if in first quadrant going to fourth.
    if(currAngle - lowerReference < 90 && adjTarget >= 270) {
      return lowerReference - (360-adjTarget);
    }
    // If it's closer to go to the upper reference, go there...
    float otherHalf = lowerReference + 180 + adjTarget; // Draw a line straight through the circle, starting at target, and ending at the other side
    if(currAngle < otherHalf) { // If you are anywhere on the right side of that line
      return lowerReference + adjTarget; // Go to the target on that side
    } else {
      return upperReference + adjTarget;
    }
  }

  // Returns the angle the character should be facing given the WASD keys pressed, from 0-360.
  public float getKeypadAngle() {
    float angle = 0;
    if(Input.GetKey(KeyCode.W)) {
      if(Input.GetKey(KeyCode.D)) angle = 45; // Handle diagonals - if other key is already pressed...
      else if(Input.GetKey(KeyCode.A)) angle = 315;
      else angle = 0; // Default
    } else if (Input.GetKey(KeyCode.D)) {
      if(Input.GetKey(KeyCode.W)) angle = 45;
      else if(Input.GetKey(KeyCode.S)) angle = 135;
      else angle = 90;
    } else if (Input.GetKey(KeyCode.S)) {
      if(Input.GetKey(KeyCode.D)) angle = 135;
      else if(Input.GetKey(KeyCode.A)) angle = 225;
      else angle = 180;
    } else if (Input.GetKey(KeyCode.A)) {
      if(Input.GetKey(KeyCode.W)) angle = 0;
      else if(Input.GetKey(KeyCode.S)) angle = 225;
      else angle = 270;
    }
    return angle;
  }

  // Update is called once per frame
  //void Update() {
  //}
}
