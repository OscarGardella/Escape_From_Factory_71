using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

// This is a modified version of a free controller script included with this Sphere Robot free asset from the Unity Assets Store.
// Its original author is Razgrizzz Demon, and the download link for this project was at
// https://assetstore.unity.com/packages/3d/characters/robots/robot-sphere-136226#reviews

// This is a class made by Justin Douty, which controls the "acceleration / deceleration" of a variable over a steady period of time 
public class Momentum {
  public float accelSpeed;
  public float target;
  private float curr = 0;

  public Momentum(float accelSpeed, float start = 0) {
    this.accelSpeed = accelSpeed;
    this.target = 0;
  }

  // Returns a value closer and closer to the target value in a linear fashion, depending on accelSpeed
  public float valueLinear() {
    float step = (Time.fixedDeltaTime * Math.Sign(target - curr) * accelSpeed);
    //if(Math.Abs(step) < Time.fixedDeltaTime * accelSpeed) { // TODO: It is a bug that this code doesn't work:
    if(Math.Abs(target - curr) < Time.fixedDeltaTime * accelSpeed) {
    // Since rounding will not be perfect to move the value to 0, the character might creep ever so slowly in random directions.
    // Not significant problem. Fix later...
      step = 0;
    }
    curr = curr + step;
    return curr;
  }

 // Returns a value closer and closer to the target value in an asymptotic fashion, depending on accelSpeed
  public float valueAsymptotic() {
    curr = curr + (Time.fixedDeltaTime * (target - curr) * accelSpeed);
    return curr;
  }
  
}

// This is a class with the same name as that of the original script by Razgrizzz Demon.
public class RobotFreeAnim : MonoBehaviour {

  // Please note that certain variables may not be updatable in the inspector while playing, as they may be checked as needed.
  Vector3 rot = Vector3.zero;
  private bool rollingEnabled = true;
  public float rotSpeed = 40f;
  private float moveSpeed;
  public float rollMoveSpeed;
  public float walkMoveSpeed;
  public float walkMomentum; // Forward movement momentum value
  public float rotationMomentum; // Y-axis rotation momentum value
  public float rollMomentumDrag; // How much to decrease rotation momentum in roll mode
  Animator anim;
  Rigidbody m_Rigidbody;
  public float cameraRotation = 0; // Angle you look at the character
  public float cameraHeight = 10; // How far away you look at the character
  public float cameraVertOffset = 0; // Height offset from character

  [Range(0.0F, 90.0F)]
  public float cameraAngle = 90;
  
  public float cameraFollowLag = 1;
  
  public Transform mainCamera = null;
  public Vector3 angles;

  Momentum walkMom;
  Momentum rotMom;
  
  // Returns true if the player is currently rolling
  public bool isRolling() {
    return anim.GetBool("Roll_Anim");
  }

  // Calculates the sine of val, in degrees. Returns it as a float.
  private float sinDegF(double val) {
    return (float) (Math.Sin(val * (Math.PI / 180.0) ));
  }

  // Calculates the cosine of val, in degrees. Returns it as a float.
  private float cosDegF(double val) {
    return (float) (Math.Cos(val * (Math.PI / 180.0) ));
  }

  //private GameObject topDownCamera; // The camera that follows the player
  // Use this for initialization
  void Awake() {
    anim = gameObject.GetComponent<Animator>();
    //anim.SetFloat("Roll_Anim", 2.0F); // Increase roll start/end animation speed
    m_Rigidbody = gameObject.GetComponent<Rigidbody>();
    walkMom = new Momentum(walkMomentum);
    rot = new Vector3(0, cameraRotation, 0);
    rotMom = new Momentum(rotationMomentum, cameraRotation); // High (lower number) asymptotic Momentum in roll mode gives a starkingly fluid effect.
    moveSpeed = walkMoveSpeed;
  }

  void Update() {
    CheckKey();
  }

  // Update is called once per frame
  void FixedUpdate() {
    
    rot[1] = rotMom.valueAsymptotic(); // Set rotation, with momentum
    gameObject.transform.eulerAngles = rot;
    //m_Rigidbody.rotation.eulerAngles = rot; // TODO is this needed?
    Vector3 posChange = transform.forward * walkMom.valueLinear(); // This describes forward motion when pressing W.
    //m_Rigidbody.position += posChange;
    m_Rigidbody.AddForce(posChange, ForceMode.VelocityChange);
    // Make main camera hover above player
    if(mainCamera != null) {  
      Vector3 cameraPos = transform.position; //- transform.forward * cosDegF(cameraAngle);

      // Calculate camera angle and height
      // Any function with Sin/Cos is related to giving a specified amount of tilt to the camera.
      // This can be used to give a slightly tilted, or even isometric view, for effect.
      float sinAngle = sinDegF(cameraAngle);
      float cosAngle = cosDegF(cameraAngle);

      cameraPos.y += cameraHeight * sinAngle + cameraVertOffset;
      cameraPos.x -= sinDegF(cameraRotation) * cameraHeight * cosAngle;
      cameraPos.z -= cosDegF(cameraRotation) * cameraHeight * cosAngle;
      //Vector3 posOffset = new Vector3(sinDegF(cameraRotation), 0, cosDegF(cameraRotation)) * cameraPos.y;
      mainCamera.position = cameraPos - posChange * cameraFollowLag;

      // Set camera rotation
      Quaternion cameraLook = new Quaternion();
      cameraLook.eulerAngles = new Vector3(cameraAngle, cameraRotation, 0);

      mainCamera.rotation = cameraLook;
    }
  }

  

  public async void enterRollMode() {
    if(isRolling()) return; // Already in roll mode
    anim.SetBool("Walk_Anim", false);
    anim.SetBool("Roll_Anim", true);
    float oldRollMoveSpeed = rollMoveSpeed; // Save old roll move speed to temporarily reduce it
    moveSpeed = walkMoveSpeed / 2; // Walk twice as slow before the roll, while the starting roll animation is playing
    walkMom.target = moveSpeed;
    await Task.Delay(900);
    rotMom.accelSpeed -= rollMomentumDrag; // A crude way to reduce roll momentum (increase slideyness) while rolling
    rotSpeed -= 10;
    if(isRolling()) {
      // Check if rolling hasn't ended by now, which would leave the speed at incorrect values. Very very small potential for a race condition,
      // but this race condition has been tested without this check to be self-correcting and non-hindering.
      walkMom.target = rollMoveSpeed;
      moveSpeed = rollMoveSpeed; // Increase to full roll move speed
    }
  }

  public void exitRollMode() {
    if(! isRolling()) return; // Not in roll mode 
    walkMom.target = walkMoveSpeed;
    anim.SetBool("Roll_Anim", false);
    moveSpeed = walkMoveSpeed;
    rotMom.accelSpeed += rollMomentumDrag;
    rotSpeed += 10;
  }
  
  public void setRollingEnabled(bool enabled) {
    rollingEnabled = enabled;
  }

  // Rolls for the specified seconds
  public async void rollFor(float seconds) {
    if(isRolling()) return;
    enterRollMode();
    await Task.Delay((int) (1000 * seconds + 900));
    exitRollMode();
  }

  // Returns whether any key is pressed corresponding to character movement
  bool movementKeyPressed() {
    return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S);
  }
  // Returns whether any key is held corresponding to character movement
  bool movementKeyHeld() {
    return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S);
  }
  // Returns whether any movement key has been released
  bool movementKeyReleased() {
    return Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S);
  }
  // Returns whether all movement keys were released (the character should not be moving)
  bool allMovementKeysReleased() {
    return movementKeyReleased() && ! movementKeyHeld();
  }


  // Returns the angle that the character would have to turn the shortest distance to face, given an arbitrary angle.
  // Finds the upper and lowermost multiples of 360 closest to currAngle. Then, decides if it is closer to move closer to the lower or upper reference.
  float getNearestAngle(float currAngle, float targetAngle) {
    currAngle = currAngle - cameraRotation;
    float adjTarget = targetAngle % 360;
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
  float getKeypadAngle() {
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

  void CheckKey() {
    // Update orientation when any movement key is pressed or released
    if(movementKeyPressed() || movementKeyReleased() && movementKeyHeld()) {
      
      float correctedAngle = getNearestAngle(rotMom.target, getKeypadAngle());
      rotMom.target = cameraRotation + correctedAngle;
      //float correctedAngle = getNearestAngle(rot[1], getKeypadAngle());
      //rotMom.target = cameraRotation + correctedAngle;
    }
    
    // Update the state machine only once per applicable keypress - handle WASD controls
    if(movementKeyPressed()) {
      anim.SetBool("Walk_Anim", true);
      if(walkMom.target != rollMoveSpeed)
        walkMom.target = walkMoveSpeed; // Walk
    }

    // Press left control while walking to enter roll mode
    if(Input.GetKeyDown(KeyCode.LeftControl) && rollingEnabled) {
      //rollFor(2.0F);
      if(movementKeyHeld()) {
        //walkMom.target = rollMoveSpeed;
        enterRollMode();
      }
    }

    if(allMovementKeysReleased()) {
      exitRollMode();
      anim.SetBool("Walk_Anim", false);
      walkMom.target = 0;
    }

      
      
    
    
    // Close - disabled for now
    /*if (Input.GetKeyDown(KeyCode.LeftControl)) {
      if (!anim.GetBool("Open_Anim")) {
        anim.SetBool("Open_Anim", true);
      } else {
        anim.SetBool("Open_Anim", false);
      }
    }*/
  }

}
