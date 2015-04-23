using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class HarryCastIdle : State, IInputKeyListener {
	
	HarryCast Layer {
		get { return ((HarryCast)layer); }
	}
    
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}

	public override void OnEnter() {
		base.OnEnter();
		
		Layer.inputSystem.GetKeyInfo("Cast").AddListener(this);
	}
	
	public override void OnExit() {
		base.OnExit();
		
		Layer.inputSystem.GetKeyInfo("Cast").RemoveListener(this);
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		Layer.UpdateCursor();
	}
	
	public void OnKeyInput(KeyInfo keyInfo, KeyStates keyState) {
		if (keyState == KeyStates.Down) {
			SwitchState<HarryCastCasting>();
		}
	}
}
