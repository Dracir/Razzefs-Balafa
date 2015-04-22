using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class ModelJumpIdle : State, IInputKeyListener {
	
	ModelJump Layer {
		get { return ((ModelJump)layer); }
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
