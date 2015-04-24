using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterRotateRotating : State {
	
	[Min] public float speed = 20;
	
	CharacterRotate Layer {
		get { return ((CharacterRotate)layer); }
	}
    
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
	
	public override void OnExit() {
		base.OnExit();
		
		Layer.Rigidbody.SetEulerAngle(Layer.targetAngle);
		Layer.spriteTransform.SetLocalScale(Layer.currentFacingDirection, Axis.X);
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if (Mathf.Abs(Layer.currentFacingDirection - Layer.targetFacingDirection) <= 0.0001F && Mathf.Abs(Layer.currentAngle - Layer.targetAngle) <= 0.0001F) {
			SwitchState("Idle");
			return;
		}
		
		Layer.currentFacingDirection = Mathf.Lerp(Layer.spriteTransform.localScale.x, Layer.targetFacingDirection, speed * Time.deltaTime);
		Layer.spriteTransform.ScaleLocalTowards(Layer.currentFacingDirection, speed, Axis.X);
	}
	
	public override void OnFixedUpdate() {
		Layer.currentAngle = Mathf.LerpAngle(transform.localEulerAngles.z, Layer.targetAngle, speed * Time.fixedDeltaTime);
		Layer.Rigidbody.RotateTowards(Layer.currentAngle, speed);
	}
}
