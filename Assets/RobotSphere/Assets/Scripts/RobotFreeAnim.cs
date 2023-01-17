using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a modified version of a free controller script included with this Sphere Robot free asset from the Unity Assets Store.
// Its original author is Razgrizzz Demon, and the download link for this project was at
// https://assetstore.unity.com/packages/3d/characters/robots/robot-sphere-136226#reviews

// This is a class made by Justin Douty, which controls the "acceleration / deceleration" of a variable over a steady period of time 
public class Momentum {
	private float accelSpeed;
	private float target = 0;
	private float curr = 0;

  public Momentum(float accelSpeed) {
		this.accelSpeed = accelSpeed;
		this.target = 0;
	}
  
	public void setTarget(float target) {
		this.target = target;
	}

  // Returns a value closer and closer to the target value in a linear fashion, depending on accelSpeed
	public float valueLinear() {
		float step = (Time.deltaTime * Math.Sign(target - curr) * accelSpeed);
	  /*if(Math.Abs(step) < Time.deltaTime * accelSpeed) { // TODO: It is a bug that this code doesn't work:
		// Since rounding will not be perfect to move the value to 0, the character might creep ever so slowly in random directions.
			step = 0;
			curr = 0;
		}*/
		curr = curr + step;
		return curr;
	}

 // Returns a value closer and closer to the target value in an asymptotic fashion, depending on accelSpeed
	public float valueAsymptotic() {
		curr = curr + (Time.deltaTime * (target - curr) * accelSpeed);
		return curr;
	}
}

public class RobotFreeAnim : MonoBehaviour {

	Vector3 rot = Vector3.zero;
	public float rotSpeed = 40f;
	public float moveSpeed;
	public float walkMomentum;
	public float rotMomentum;
	Animator anim;
  Rigidbody m_Rigidbody;

  Momentum walkMom;
	Momentum rotMom;

	// Use this for initialization
	void Awake() {
		anim = gameObject.GetComponent<Animator>();
		gameObject.transform.eulerAngles = rot;
		m_Rigidbody = gameObject.GetComponent<Rigidbody>();
		walkMom = new Momentum(walkMomentum);

	}

	// Update is called once per frame
	void Update() {
		CheckKey();
		gameObject.transform.eulerAngles = rot;
		transform.position += transform.forward * walkMom.valueLinear();
	}

	void CheckKey() {
		// Walk
		if (Input.GetKey(KeyCode.W)) {
			anim.SetBool("Walk_Anim", true);
			walkMom.setTarget(moveSpeed);
			//transform.position += transform.forward * mom.value() * Time.fixedDeltaTime;
		}
		else if (Input.GetKeyUp(KeyCode.W)) {
			walkMom.setTarget(0);
			anim.SetBool("Walk_Anim", false);
		}

		// Rotate Left
		if (Input.GetKey(KeyCode.A)) {
			// The variable "rot" is a Vector3, which is read upon every update frame to set the rotation direction.
			// Incrementing or decrementing this variable around the y-axis, in effect, rotates it laterally, over a fixed time amount.
			rot[1] -= rotSpeed * Time.fixedDeltaTime;
		}

		// Rotate Right
		if (Input.GetKey(KeyCode.D)) {
			rot[1] += rotSpeed * Time.fixedDeltaTime;
		}

		// Roll
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (anim.GetBool("Roll_Anim")) {
				anim.SetBool("Roll_Anim", false);
			} else {
				anim.SetBool("Roll_Anim", true);
			}
		}

		// Close
		if (Input.GetKeyDown(KeyCode.LeftControl)) {
			if (!anim.GetBool("Open_Anim")) {
				anim.SetBool("Open_Anim", true);
			} else {
				anim.SetBool("Open_Anim", false);
			}
		}
	}

}
