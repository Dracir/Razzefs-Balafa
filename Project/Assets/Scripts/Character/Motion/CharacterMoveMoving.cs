using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterMoveMoving : State {
	
	[Min] public float speed = 3;
	[Min] public float acceleration = 100;
	[Min] public float inputPower = 1;
	[Disable] public float currentSpeed;
	[Disable] public float currentAcceleration;
	
	CharacterMove Layer {
		get { return ((CharacterMove)layer); }
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if (Layer.AbsHorizontalAxis <= Layer.moveThreshold && Layer.AbsMoveVelocity <= 0) {
			SwitchState("Idle");
		}
	}
	
	public override void OnFixedUpdate() {
		currentSpeed = Layer.HorizontalAxis.PowSign(inputPower) * speed * (1F / Mathf.Max(Mathf.Sqrt(Layer.Friction), 0.0001F));
		currentAcceleration = Mathf.Max(Layer.Friction, 0.001F) * acceleration;
		
		if (Layer.Gravity.Angle == 90) {
			Layer.rigidbody.AccelerateTowards(currentSpeed, currentAcceleration, Axis.X);
			Layer.MoveVelocity = Layer.rigidbody.velocity.x;
		}
		else if (Layer.Gravity.Angle == 180) {
			Layer.rigidbody.AccelerateTowards(-currentSpeed, currentAcceleration, Axis.Y);
			Layer.MoveVelocity = Layer.rigidbody.velocity.y;
		}
		else if (Layer.Gravity.Angle == 270) {
			Layer.rigidbody.AccelerateTowards(-currentSpeed, currentAcceleration, Axis.X);
			Layer.MoveVelocity = Layer.rigidbody.velocity.x;
		}
		else if (Layer.Gravity.Angle == 0) {
			Layer.rigidbody.AccelerateTowards(currentSpeed, currentAcceleration, Axis.Y);
			Layer.MoveVelocity = Layer.rigidbody.velocity.y;
		}
		else {
			Vector3 velocity = Layer.rigidbody.velocity.Rotate(-Layer.Gravity.Angle + 90);
			velocity.x = Mathf.Lerp(velocity.x, currentSpeed, Time.fixedDeltaTime * speed * currentAcceleration);
			Layer.MoveVelocity = velocity.x;
			velocity = velocity.Rotate(Layer.Gravity.Angle - 90);
			
			Layer.rigidbody.SetVelocity(velocity);
		}
	}
}
