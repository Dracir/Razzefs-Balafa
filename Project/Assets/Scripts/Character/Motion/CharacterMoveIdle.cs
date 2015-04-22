using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterMoveIdle : State {
	
	CharacterMove Layer {
		get { return ((CharacterMove)layer); }
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if (Layer.AbsHorizontalAxis > Layer.moveThreshold || Layer.AbsMoveVelocity > 0) {
			SwitchState("Moving");
			return;
		}
		
		if (Layer.Gravity.Angle == 90) {
			Layer.MoveVelocity = Layer.rigidbody.velocity.x;
		}
		else if (Layer.Gravity.Angle == 180) {
			Layer.MoveVelocity = Layer.rigidbody.velocity.y;
		}
		else if (Layer.Gravity.Angle == 270) {
			Layer.MoveVelocity = Layer.rigidbody.velocity.x;
		}
		else if (Layer.Gravity.Angle == 0) {
			Layer.MoveVelocity = Layer.rigidbody.velocity.y;
		}
		else {
			Layer.MoveVelocity = Layer.rigidbody.velocity.Rotate(-Layer.Gravity.Angle + 90).x;
		}
	}
}
