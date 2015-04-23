using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterRotateIdle : State {
	
	CharacterRotate Layer {
		get { return ((CharacterRotate)layer); }
	}
    
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if (Mathf.Abs(Layer.currentFacingDirection - Layer.targetFacingDirection) > 0.0001F || Mathf.Abs(Layer.currentAngle - Layer.targetAngle) > 0.0001F) {
			SwitchState("Rotating");
		}
	}
}
