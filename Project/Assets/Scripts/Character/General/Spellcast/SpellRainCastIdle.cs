using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellRainCastIdle : State, IInputKeyListener {
	
    SpellRainCast Layer {
    	get { return (SpellRainCast)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
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
			SwitchState<SpellRainCastCasting>();
		}
	}
}
