using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks; // UniTask (https://github.com/Cysharp/UniTask)

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
  public bool rollingEnabled = true;
  private float moveSpeed;
  public float rollMoveSpeed;
  public float walkMoveSpeed;
  public float walkMomentum; // Forward movement momentum value
  public float rotationMomentum; // Y-axis rotation momentum value
  public float rollMomentumDrag; // How much to decrease rotation momentum in roll mode
  public bool cameraLockEnabled = false;
  Animator anim;
  Rigidbody m_Rigidbody;
  public float cameraRotation = 0; // Angle the camera looks at the player. Also changes the direction of the controls.
  public float cameraHeight = 10; // How far away you look at the character
  public float cameraVertOffset = 0; // Height offset from character

  [Range(0.0F, 90.0F)]
  public float cameraAngle = 90;
  public float cameraFollowLag = 1;

  public InputControls controls;
  
  public Camera mainCamera = null;
  public Vector3 angles;

  Momentum walkMom;
  Momentum rotMom;

  // Returns true if the player is currently rolling
  public bool isRolling() {
    return anim.GetBool("Roll_Anim");
  }

  public bool isOpen() {
    return anim.GetBool("Open_Anim");
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
    if(! anim) Debug.Log("RobotFreeAnim.cs: Error: failed to get animator component of player");
    //anim.SetFloat("Roll_Anim", 2.0F); // Increase roll start/end animation speed
    m_Rigidbody = gameObject.GetComponent<Rigidbody>();
    if(! m_Rigidbody) Debug.Log("RobotFreeAnim.cs: Error: failed to get RigitBody component of player");
    walkMom = new Momentum(walkMomentum);
    rot = new Vector3(0, cameraRotation, 0);
    rotMom = new Momentum(rotationMomentum, cameraRotation); // High (lower number) asymptotic Momentum in roll mode gives a starkingly fluid effect.
    moveSpeed = walkMoveSpeed;
    controls = new InputControls();
    if(! mainCamera) Debug.Log("RobotFreeAnim.cs: Error: No camera specified. This script requires a connection target to the main camera in the scene.");
  }

  // Activates this character from its normally closed and idle state
  public void open() {
    anim.SetBool("Open_Anim", true);
  }

  public void close() {
     anim.SetBool("Open_Anim", false);
  }

  // Sets the camera position relative to the player
  private void setCameraPos(float angle, float height, float vertOffset, Vector3 offset) {
    if(mainCamera != null) {  
      Vector3 cameraPos = transform.position; //- transform.forward * cosDegF(cameraAngle);

      // Set camera rotation based on provided parameters and global camera rotation (global camera rotation basically makes angles "transparent" to the orientation of the player).
      Quaternion cameraLook = new Quaternion();
      cameraLook.eulerAngles = new Vector3(angle, cameraRotation, 0); // Apply local angle and global camera rotation
      mainCamera.transform.rotation = cameraLook;

      if(angle == 90) { // Optimization - avoid expensive sin/cos calculations for a "normal" top-down camera orientation.
        cameraPos.y += height + vertOffset;
        mainCamera.transform.position = cameraPos - offset;
        return;
      }
      // Calculate camera angle and height
      // Any function with Sin/Cos is related to giving a specified amount of tilt to the camera.
      // This can be used to give a slightly tilted, or even isometric view, for effect.
      float sinAngle = sinDegF(angle);
      float cosAngle = cosDegF(angle);

      cameraPos.y += height * sinAngle + vertOffset;
      // instance var cameraRotation will always make this relative to the player
      cameraPos.x -= sinDegF(cameraRotation) * height * cosAngle;
      cameraPos.z -= cosDegF(cameraRotation) * height * cosAngle;
      //Vector3 posOffset = new Vector3(sinDegF(cameraRotation), 0, cosDegF(cameraRotation)) * cameraPos.y;
      mainCamera.transform.position = cameraPos - offset;

    }
  }

  void Update() {
    if(anim.GetBool("Open_Anim") == false) return;
    CheckKey();
  }

  // Update is called once per frame
  void FixedUpdate() {
    if(anim.GetBool("Open_Anim") == false) {
      m_Rigidbody.AddForce(new Vector3(0,-0.05f,0), ForceMode.VelocityChange); // Keeps it from floating off the ground
      return;
    }
    controls.globalOrientation = cameraRotation;
    rot[1] = rotMom.valueAsymptotic(); // Set rotation, with momentum
    gameObject.transform.eulerAngles = rot;
    Vector3 posChange = transform.forward * walkMom.valueLinear(); // This describes forward motion when pressing W.
    m_Rigidbody.AddForce(posChange, ForceMode.VelocityChange); 
    
    // Make main camera hover above player
    if(cameraLockEnabled) setCameraPos(cameraAngle, cameraHeight, cameraVertOffset, posChange * cameraFollowLag);
  }

  

  public async UniTask enterRollMode() {
    // TODO: in condition: || pauseGame.paused
    if(isRolling()) return; // Already in roll mode
    anim.SetBool("Walk_Anim", false);
    anim.SetBool("Roll_Anim", true);
    float oldRollMoveSpeed = rollMoveSpeed; // Save old roll move speed to temporarily reduce it
    moveSpeed = walkMoveSpeed / 2; // Walk twice as slow before the roll, while the starting roll animation is playing
    walkMom.target = moveSpeed;
    
    await UniTask.Delay(900);
    rotMom.accelSpeed -= rollMomentumDrag; // A crude way to reduce roll momentum (increase slideyness) while rolling
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
  }
  

  // Rolls for the specified seconds
  public async void rollFor(float seconds) {
    if(isRolling()) return;
    _ = enterRollMode();
    await Task.Delay((int) (1000 * seconds + 900));
    exitRollMode();
  }

  

  void CheckKey() {
    // Update the state machine only once per applicable keypress - handle WASD controls
    if(controls.startedMoving()) {
      if(walkMom.target != rollMoveSpeed) {
        walkMom.target = walkMoveSpeed; // Walk -- TODO: Why doesn't this hold?
        anim.SetBool("Walk_Anim", true);
      }
    }

    // Update orientation when any movement key is pressed or released. Note that this should be placed after the startedMoving check.
    rotMom.target = controls.getFacing(rot[1]); // Based on where I am currently facing, where do the controls indicate I should face? Let me just slowly face there...

    // Press left control while walking to enter roll mode
    // deprecated
    if(Input.GetKeyDown(KeyCode.LeftControl) && rollingEnabled) {
      //rollFor(2.0F);
      if(controls.isMoving()) {
        //walkMom.target = rollMoveSpeed;
        _ = enterRollMode();
      }
    }

    if(controls.stoppedMoving()) { // && !pauseGame.paused
      exitRollMode();
      walkMom.target = 0;
      anim.SetBool("Walk_Anim", false);
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
