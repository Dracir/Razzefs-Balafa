using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class ModelMoveMoving : State {
	
	[Min] public float speed = 3;
	[Min] public float acceleration = 100;
	[Min] public float inputPower = 1;
	[Disable] public float currentSpeed;
	
	ModelMove Layer {
		get { return ((ModelMove)layer); }
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if (Layer.AbsHorizontalAxis <= Layer.moveThreshold) {
			SwitchState("Idle");
		}
	}
	
	public override void OnFixedUpdate() {
		currentSpeed = Layer.HorizontalAxis.PowSign(inputPower) * speed;
		
		if (Layer.Gravity.Angle == 90) {
			Layer.rigidbody.AccelerateTowards(currentSpeed, acceleration, Axis.X);
		}
		else if (Layer.Gravity.Angle == 180) {
			Layer.rigidbody.AccelerateTowards(-currentSpeed, acceleration, Axis.Y);
		}
		else if (Layer.Gravity.Angle == 270) {
			Layer.rigidbody.AccelerateTowards(-currentSpeed, acceleration, Axis.X);
		}
		else if (Layer.Gravity.Angle == 0) {
			Layer.rigidbody.AccelerateTowards(currentSpeed, acceleration, Axis.Y);
		}
		else {
			Vector3 velocity = Layer.rigidbody.velocity.Rotate(-Layer.Gravity.Angle + 90);
			velocity.x = Mathf.Lerp(velocity.x, currentSpeed, Time.fixedDeltaTime * speed);
			velocity = velocity.Rotate(Layer.Gravity.Angle - 90);
			
			Layer.rigidbody.SetVelocity(velocity);
		}
	}
}
