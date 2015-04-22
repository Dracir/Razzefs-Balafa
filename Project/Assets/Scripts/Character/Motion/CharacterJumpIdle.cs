using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterJumpIdle : State, IInputKeyListener {
	
	CharacterJump Layer {
		get { return ((CharacterJump)layer); }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		Layer.inputSystem.GetKeyInfo("Jump").AddListener(this);
	}
	
	public override void OnExit() {
		base.OnExit();
		
		Layer.inputSystem.GetKeyInfo("Jump").RemoveListener(this);
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if (!Layer.Grounded) {
			SwitchState("Falling");
			return;
		}
	}
	
	public void OnKeyInput(KeyInfo keyInfo, KeyStates keyState) {
		if (keyState == KeyStates.Down) {
			SwitchState("Jumping");
		}
	}
}
