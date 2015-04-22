using UnityEngine;
using System.Collections;

public class Player : Movable {

	// Use this for initialization
	protected override void Start () {
		base.Start();

		SetVariables (
			true, // hasGravity
			5f, //walkSpeed
			2f, //runspeed
			0.5f, // time to reach max speed
			10f, // jump impulse
			10f, // fall speed
			1f, // gravity modifier
			0.2f //time taken to stop completely
			);
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		ApplyGravity();
		
		controller.GetInputs();
		
		int hInput = 0;
		if (controller.getR)
			hInput ++;
		if (controller.getL)
			hInput --;
		
		velocity = Walk(hInput);
		
		if (controller.getJumpDown && grounded){
			velocity = Jump();
		}
		ApplyVelocity();
	}
}
