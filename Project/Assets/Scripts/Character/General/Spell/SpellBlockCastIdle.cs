using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellBlockCastIdle : State, IInputKeyListener {
	
    SpellBlockCast Layer {
    	get { return (SpellBlockCast)layer; }
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
			SwitchState<SpellBlockCastCasting>();
		}
	}
}
