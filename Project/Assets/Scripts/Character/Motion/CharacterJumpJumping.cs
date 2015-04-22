using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterJumpJumping : State, IInputKeyListener {
	
	[Min] public float minHeight = 1;
	[Min] public float maxHeight = 30;
	[Min] public float duration = 0.25F;
	
	[Disable] public float counter;
	[Disable] public float increment;
	[Disable] public Vector2 direction;
	
	CharacterJump Layer {
		get { return ((CharacterJump)layer); }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		counter = duration;
		increment = (maxHeight - minHeight) / duration;
		direction = -Layer.Gravity.Direction;
		
		Layer.inputSystem.GetKeyInfo("Jump").AddListener(this);
		Layer.Jumping = true;
		Layer.animator.Play(Layer.jumpingHash, 1);
		
		if (Layer.Gravity.Angle == 90) {
			Layer.rigidbody.SetVelocity(minHeight, Axis.Y);
		}
		else if (Layer.Gravity.Angle == 180) {
			Layer.rigidbody.SetVelocity(minHeight, Axis.X);
		}
		else if (Layer.Gravity.Angle == 270) {
			Layer.rigidbody.SetVelocity(-minHeight, Axis.Y);
		}
		else if (Layer.Gravity.Angle == 0) {
			Layer.rigidbody.SetVelocity(-minHeight, Axis.X);
		}
		else {
			Vector3 velocity = Layer.rigidbody.velocity.Rotate(-Layer.Gravity.Angle + 90);
			velocity.y = minHeight;
			velocity = velocity.Rotate(Layer.Gravity.Angle - 90);
			
			Layer.rigidbody.SetVelocity(velocity);
		}
	}
	
	public override void OnExit() {
		base.OnExit();
		
		Layer.inputSystem.GetKeyInfo("Jump").RemoveListener(this);
		Layer.Jumping = false;
	}

	public override void OnFixedUpdate() {
		base.OnFixedUpdate();
		
		counter -= Time.fixedDeltaTime;
		
		if (counter > 0) {
			Layer.rigidbody.Accelerate(direction * increment * (counter / duration), Axis.XY);
		}
		else {
			SwitchState("Falling");
		}
	}
	
	public void OnKeyInput(KeyInfo keyInfo, KeyStates keyState) {
		if (keyState != KeyStates.Pressed) {
			SwitchState("Falling");
		}
	}
}
