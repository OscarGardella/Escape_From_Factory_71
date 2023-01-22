using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a modified version of a free controller script included with this Sphere Robot free asset from the Unity Assets Store.
// Its original author is Razgrizzz Demon, and the download link for this project was at
// https://assetstore.unity.com/packages/3d/characters/robots/robot-sphere-136226#reviews

// This is a class made by Justin Douty, which controls the "acceleration / deceleration" of a variable over a steady period of time 
public class Momentum {
  public float accelSpeed;
  public float target;
  private float curr = 0;

  public Momentum(float accelSpeed) {
    this.accelSpeed = accelSpeed;
    this.target = 0;
  }


  // Returns a value closer and closer to the target value in a linear fashion, depending on accelSpeed
  public float valueLinear() {
    float step = (Time.deltaTime * Math.Sign(target - curr) * accelSpeed);
    //if(Math.Abs(step) < Time.deltaTime * accelSpeed) { // TODO: It is a bug that this code doesn't work:
    if(Math.Abs(target - curr) < Time.deltaTime * accelSpeed) {
    // Since rounding will not be perfect to move the value to 0, the character might creep ever so slowly in random directions.
    // Not significant problem. Fix later...
      step = 0;
    }
    curr = curr + step;
    return curr;
  }

 // Returns a value closer and closer to the target value in an asymptotic fashion, depending on accelSpeed
  public float valueAsymptotic() {
    curr = curr + (Time.deltaTime * (target - curr) * accelSpeed);
    return curr;
  }
}

// This is a class with the same name as that of the original script by Razgrizzz Demon.
public class RobotFreeAnim : MonoBehaviour {

  // Please note that certain variables may not be updatable in the inspector while playing, as they may be checked as needed.
  Vector3 rot = Vector3.zero;
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

  Momentum walkMom;
  Momentum rotMom;

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
    gameObject.transform.eulerAngles = rot;
    m_Rigidbody = gameObject.GetComponent<Rigidbody>();
    walkMom = new Momentum(walkMomentum);
    rotMom = new Momentum(rotationMomentum); // High (lower number) asymptotic Momentum in roll mode gives a starkingly fluid effect.
    moveSpeed = walkMoveSpeed;

    // Get top-down camera
    //topDownCamera = gameObject.GetComponent<Camera>();
    //topDownCamera = gameObject.GetComponent<GameObject>();
    //Debug.Log(gameObject.GetComponents<Camera>());
  }

  // Update is called once per frame
  void Update() {
    CheckKey();
    rot[1] = rotMom.valueAsymptotic(); // Set rotation, with momentum
    gameObject.transform.eulerAngles = rot;
    Vector3 posChange = transform.forward * walkMom.valueLinear(); // This describes forward motion when pressing W.
    transform.position += posChange;
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

  void enterRollMode() {
    if(anim.GetBool("Roll_Anim")) return; // Already in roll mode
    anim.SetBool("Walk_Anim", false);
    anim.SetBool("Roll_Anim", true);
    moveSpeed = rollMoveSpeed;
    rotMom.accelSpeed -= rollMomentumDrag;
    rotSpeed -= 10;
  }

  void exitRollMode() {
    if(! anim.GetBool("Roll_Anim")) return; // Not in roll mode 
    anim.SetBool("Roll_Anim", false);
    moveSpeed = walkMoveSpeed;
    rotMom.accelSpeed += rollMomentumDrag;
    rotSpeed += 10;
  }

  void CheckKey() {

    // Update the state machine only once per applicable keypress
    if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W)) anim.SetBool("Walk_Anim", true);
    else if((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W)) && !(Input.GetKey(KeyCode.W))) anim.SetBool("Walk_Anim", false);
    
    // Rotate left while key is currently held down
    if (Input.GetKey(KeyCode.A)) {
      // The variable "rot" is a Vector3, which is read upon every update frame to set the rotation direction.
      // Incrementing or decrementing this variable around the y-axis, in effect, rotates it laterally, over a fixed time amount.
      // Using my Momentum class also smooths this to make the movement look more fluid and natural.
      rotMom.target = rotMom.target - rotSpeed * Time.deltaTime;
    } 
    
    if (Input.GetKey(KeyCode.D)) { // Rotate Right
      rotMom.target = rotMom.target + rotSpeed * Time.deltaTime;
    } 
    
    // Press left control while walking to enter roll mode
    if(Input.GetKey(KeyCode.LeftControl)) {
      // If W and space are pressed at once
      if(Input.GetKey(KeyCode.W)) {
        walkMom.target = rollMoveSpeed;
        enterRollMode();
      }
    } else if(Input.GetKeyDown(KeyCode.W)) { // Walk
      walkMom.target = walkMoveSpeed;
    } else if (Input.GetKeyUp(KeyCode.W)) {
      exitRollMode();
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
