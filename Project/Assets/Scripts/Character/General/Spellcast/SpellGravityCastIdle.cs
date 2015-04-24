using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellGravityCastIdle : State, IInputKeyListener {
	
	SpellGravityCast Layer {
		get { return ((SpellGravityCast)layer); }
	}
    
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}

	public override void OnEnter() {
		base.OnEnter();
		
		Layer.InputSystem.GetKeyInfo("Cast").AddListener(this);
	}
	
	public override void OnExit() {
		base.OnExit();
		
		Layer.InputSystem.GetKeyInfo("Cast").RemoveListener(this);
	}
	
	public void OnKeyInput(KeyInfo keyInfo, KeyStates keyState) {
		if (keyState == KeyStates.Down) {
			SwitchState<SpellGravityCastCasting>();
		}
	}
}
