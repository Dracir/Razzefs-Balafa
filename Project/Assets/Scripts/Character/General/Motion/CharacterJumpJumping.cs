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
		
		Layer.InputSystem.GetKeyInfo("Jump").AddListener(this);
		Layer.Jumping = true;
		Layer.Animator.Play(Layer.JumpingHash, 1);
		
		if (Layer.Gravity.Angle == 90) {
			Layer.Rigidbody.SetVelocity(minHeight, Axis.Y);
		}
		else if (Layer.Gravity.Angle == 180) {
			Layer.Rigidbody.SetVelocity(minHeight, Axis.X);
		}
		else if (Layer.Gravity.Angle == 270) {
			Layer.Rigidbody.SetVelocity(-minHeight, Axis.Y);
		}
		else if (Layer.Gravity.Angle == 0) {
			Layer.Rigidbody.SetVelocity(-minHeight, Axis.X);
		}
		else {
			Vector3 velocity = Layer.Rigidbody.velocity.Rotate(-Layer.Gravity.Angle + 90);
			velocity.y = minHeight;
			velocity = velocity.Rotate(Layer.Gravity.Angle - 90);
			
			Layer.Rigidbody.SetVelocity(velocity);
		}
	}
	
	public override void OnExit() {
		base.OnExit();
		
		Layer.InputSystem.GetKeyInfo("Jump").RemoveListener(this);
		Layer.Jumping = false;
	}

	public override void OnFixedUpdate() {
		base.OnFixedUpdate();
		
		counter -= Time.fixedDeltaTime;
		
		if (counter > 0) {
			Layer.Rigidbody.Accelerate(direction * increment * (counter / duration), Axis.XY);
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
